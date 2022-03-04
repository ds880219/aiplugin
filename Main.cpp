#include "IllustratorSDK.h"
#include "Plugin.hpp"

#include <string.h>
#include <stdio.h>
#include <iostream>
#include <fstream>
#include <Windows.h>
#include <SPSuites.h>

using namespace std;

bool flag = true;
Plugin* plugin;
AIActionManagerSuite* sAIActionManager;
AISwatchGroupSuite* sAISwatchGroup;
AISwatchListSuite* sAISwatchList;

ai::int32 ErrorCode;
const char* eventName;
AIActionParamValueRef parameters;
long Key;
int Value;

extern "C"
{
	SPBasicSuite* sSPBasic;
}

void ExecuteAction()
{
	try
	{
		if (sAIActionManager)
		{
			ErrorCode = sAIActionManager->PlayActionEvent(eventName, kDialogNone, parameters);
			sAIActionManager->AIDeleteActionParamValue(parameters);
			flag = true;
		}
	}
	catch (const std::exception& s)
	{
		cout << s.what();
	}
}

//void ExecuteActionSwatch()
//{
//	if (sAISwatchGroup && sAISwatchList)
//	{
//		ai::int32 indexOfSwatchGroup = 0;
//		ai::int32 numberOfSwatches = 0;
//		AISwatchListRef list;
//		AISwatchGroupRef swatchGroup;
//		ASBoolean deleteCustomColor = false;
//
//		sAISwatchList->GetSwatchList(NULL, &list);
//		indexOfSwatchGroup = sAISwatchGroup->CountSwatchGroups(list);
//		for (int i = 0; i < indexOfSwatchGroup; i++)
//		{
//			swatchGroup = sAISwatchGroup->GetNthSwatchGroup(list, i);
//			numberOfSwatches = sAISwatchGroup->CountSwatches(swatchGroup);
//			for (int j = 0; j < numberOfSwatches; j++)
//			{
//				sAISwatchGroup->RemoveNthSwatch(swatchGroup, j, deleteCustomColor, true);
//			}
//		}
//	}
//}

void InitializeAction(long key, int value)
{
	if (flag)
	{
		sAIActionManager->AINewActionParamValue(&parameters);
		flag = false;
	}
	sAIActionManager->AIActionSetInteger(parameters, key, value);
}

void RegisterAction()
{
	try
	{
		ifstream file;
		file.open("..\\..\\..\\param.txt");
		string name;
		if (file)
		{
			if (file.is_open())
			{
				char paramdata[300];
				bool eventnameflag = true;
				int keyflag = 0;
				while (file.getline(paramdata, 300))
				{
					if (eventnameflag)
					{
						name = paramdata;
						eventnameflag = false;
					}
					else
					{
						if (keyflag == 0)
						{
							Key = atol(paramdata);
							keyflag++;
						}
						else
						{
							Value = atol(paramdata);
							InitializeAction(Key, Value);
							keyflag = 0;
						}
					}
				}
				eventName = name.c_str();
				ExecuteAction();
			}
			else
			{
				cout << "unable to open file";
			}
		}
		else
		{
			cout << "file doesn't exist";
		}
		file.close();
		if (remove("..\\..\\..\\param.txt") == 0) cout << "File deleted successfully";
		else cout << "File not deleted or dosen't exist";
	}
	catch (const std::exception& s)
	{
		cout << s.what();
	}
}

extern Plugin* AllocatePlugin(SPPluginRef pluginRef);
extern void FixupReload(Plugin* plugin);


extern "C" ASAPI ASErr PluginMain(char* caller, char* selector, void* message)
{
	const void* Action;
	const void* Group;
	const void* List;
	ASErr error = kNoErr;
	SPMessageData* msgData = (SPMessageData*)message;

	plugin = (Plugin*)msgData->globals;

	sSPBasic = msgData->basic;
	sSPBasic->AcquireSuite(kAIActionManagerSuite, kAIActionManagerVersion, &Action);
	sAIActionManager = (AIActionManagerSuite*)Action;

	/*sSPBasic->AcquireSuite(kAISwatchGroupSuite, kAISwatchGroupVersion, &Group);
	sAISwatchGroup = (AISwatchGroupSuite*)Group;

	sSPBasic->AcquireSuite(kAISwatchListSuite, kAISwatchListVersion, &List);
	sAISwatchList = (AISwatchListSuite*)List;*/

	if (strcmp(caller, kSPInterfaceCaller) == 0)
	{
		if (strcmp(selector, kSPInterfaceStartupSelector) == 0)
		{
			plugin = AllocatePlugin(msgData->self);
			if (plugin)
			{
				msgData->globals = (void*)plugin;
				error = plugin->StartupPlugin((SPInterfaceMessage*)message);

				if (error != kNoErr)
				{
					// Make sure to delete in case startup failed
					delete plugin;
					plugin = nullptr;
					msgData->globals = nullptr;
				}
			}
			else
			{
				error = kOutOfMemoryErr;
			}
		}
		else if (strcmp(selector, kSPInterfaceShutdownSelector) == 0)
		{
			if (plugin)
			{
				error = plugin->ShutdownPlugin((SPInterfaceMessage*)message);
				delete plugin;
				plugin = nullptr;
				msgData->globals = nullptr;
			}
		}
	}

	if (plugin)
	{
		if (Plugin::IsReloadMsg(caller, selector))
		{
			// Call this before calling any virtual functions (like Message)
			FixupReload(plugin);
			error = plugin->ReloadPlugin((SPInterfaceMessage*)message);
		}
		else
		{
			// If a load or reload failed because the suites could not be acquired, we released
			// any partially acquired suites and returned an error.  However, SuitePea still might
			// call us, so protect against this situation.
			//if (plugin->SuitesAcquired())
				//error = plugin->Message(caller, selector, message);
			//else
				//error = kNoErr;
			RegisterAction();
		}

		if (error == kUnhandledMsgErr)
		{
			error = kNoErr;
#ifndef NDEBUG
#ifdef MAC_ENV
			fprintf(stderr, "Warning: Unhandled plugin message: caller \"%s\" selector \"%s\"\n", caller, selector);
#else
			char buf[1024];

			sprintf(buf + 1, "Warning: Unhandled plugin message: caller \"%s\" selector \"%s\"\n", caller, selector);
			OutputDebugStringA(buf + 1);
#endif
#endif
		}
	}

	if (error)
	{
		if (plugin)
			plugin->ReportError(error, caller, selector, message);
		else
			Plugin::DefaultError(msgData->self, error);
	}

	return error;
}
