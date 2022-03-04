using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IllustratorCLI
{
    public struct ActionData
    {
        public string InternalName;
        public KeyValue[] Parameters;
    }
    public struct KeyValue
    {
        public long Key;
        public int Value;
    }

    class BEL_Actions
    {
        public Dictionary<string, ActionData> actions = new Dictionary<string, ActionData>();

        public void AIAction()
        {
            actions.Add("Apply Spot Color", new ActionData
            {
                InternalName = "ai_plugin_swatches",
                Parameters = new KeyValue[]
                {
                    new KeyValue
                    {
                        Key = 1937204072,
                        Value = 4
                    }
                }
            });
            actions.Add("Apply Spot Black", new ActionData
            {
                InternalName = "ai_plugin_swatches",
                Parameters = new KeyValue[]
                {
                    new KeyValue
                    {
                        Key = 1937204072,
                        Value = 9
                    }
                }
            });
            actions.Add("Align Stroke Outside", new ActionData
            {
                InternalName = "ai_plugin_setStroke",
                Parameters = new KeyValue[]
                {
                    new KeyValue
                    {
                        Key = 2003072104,
                        Value = 1
                    },
                    new KeyValue
                    {
                        Key = 1667330094,
                        Value = 0
                    },
                    new KeyValue
                    {
                        Key = 1836344690,
                        Value = 10
                    },
                    new KeyValue
                    {
                        Key = 1785686382,
                        Value = 0
                    },
                    new KeyValue
                    {
                        Key = 1684825454,
                        Value = 0
                    },
                    new KeyValue
                    {
                        Key = 1684104298,
                        Value = 0
                    },
                    new KeyValue
                    {
                        Key = 1634231345,
                        Value = 6
                    },
                    new KeyValue
                    {
                        Key = 1634231346,
                        Value = 6
                    },
                    new KeyValue
                    {
                        Key = 1634230636,
                        Value = 0
                    },
                    new KeyValue
                    {
                        Key = 1634494318,
                        Value = 2
                    }
                }
            });
            actions.Add("Rasterize Text", new ActionData
            {
                InternalName = "ai_plugin_rasterize",
                Parameters = new KeyValue[]
                {
                    new KeyValue
                    {
                        Key = 1668246642,
                        Value = 5
                    },
                    new KeyValue
                    {
                        Key = 1685088558,
                        Value = 300
                    },
                    new KeyValue
                    {
                        Key = 1651205988,
                        Value = 1
                    },
                    new KeyValue
                    {
                        Key = 1954115685,
                        Value = 0
                    },
                    new KeyValue
                    {
                        Key = 1634494763,
                        Value = 1
                    },
                    new KeyValue
                    {
                        Key = 1835103083,
                        Value = 0
                    },
                    new KeyValue
                    {
                        Key = 1886613620,
                        Value = 1
                    },
                    new KeyValue
                    {
                        Key = 1885430884,
                        Value = 0
                    }
                }
            });
            actions.Add("Apply Engraving Swatch", new ActionData
            {
                InternalName = "ai_plugin_swatches",
                Parameters = new KeyValue[]
                {
                    new KeyValue
                    {
                        Key = 1937204072,
                        Value = 8
                    }
                }
            });
            actions.Add("Apply Engraving Graphic Style", new ActionData
            {
                InternalName = "ai_plugin_styles",
                Parameters = new KeyValue[]
                {
                    new KeyValue
                    {
                        Key = 1937013100,
                        Value = 8
                    },
                    new KeyValue
                    {
                        Key = 1633969268,
                        Value = 0
                    }
                }
            });
            actions.Add("Duplicate Layer", new ActionData
            {
                InternalName = "ai_plugin_Layer",
                Parameters = new KeyValue[]
                {
                    new KeyValue
                    {
                        Key = 1836411236,
                        Value = 1
                    },
                    new KeyValue
                    {
                        Key = 1851878757,
                        Value = 19
                    }
                }
            });
            actions.Add("CTS Layer Spot Black", new ActionData
            {
                InternalName = "ai_plugin_swatches",
                Parameters = new KeyValue[]
                {
                    new KeyValue
                    {
                        Key = 1937204072,
                        Value = 9
                    }
                }
            });
            actions.Add("CTS Layer Spot Black 1", new ActionData
            {
                InternalName = "ai_plugin_swatches",
                Parameters = new KeyValue[]
                {
                    new KeyValue
                    {
                        Key = 1937204072,
                        Value = 9
                    }
                }
            });
            actions.Add("RemoveClippingMask", new ActionData
            {
                InternalName = "ai_plugin_selectmask",
                Parameters = new KeyValue[]
                {
                    new KeyValue
                    {
                        Key = 1851878757,
                        Value = 14
                    }
                }
            });
            actions.Add("Remove Unused Swatches", new ActionData
            {
                InternalName = "ai_plugin_swatches",
                Parameters = new KeyValue[]
                {
                    new KeyValue
                    {
                        Key = 1835363957,
                        Value = 3
                    }
                }
            });
            actions.Add("Select Unused Swatches", new ActionData
            {
                InternalName = "ai_plugin_swatches",
                Parameters = new KeyValue[]
                {
                    new KeyValue
                    {
                        Key = 1835363957,
                        Value = 11
                    }
                }
            });
            actions.Add("Select All", new ActionData
            {
                InternalName = "adobe_selectAll",
            });
            actions.Add("Clear", new ActionData
            {
                InternalName = "adobe_clear",
            });
        }
    }
}
