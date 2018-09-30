﻿using Ludiq;
using Bolt;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

namespace Bolt.Addons.Community.Fundamentals
{
    [Descriptor(typeof(BranchParams))]
    public class BranchParamsDescriptor : UnitDescriptor<BranchParams>
    {
        public BranchParamsDescriptor(BranchParams unit) : base(unit) { }

        protected override EditorTexture DefinedIcon()
        {
            switch (unit.BranchingType)
            {
                case LogicParamNode.BranchType.And:
                    return EditorTexture.Load(new AssemblyResourceProvider(Assembly.GetExecutingAssembly(), "Bolt.Addons.Community.Fundamentals.Editor", "Resources"), "Branch_And.png", CreateTextureOptions.PixelPerfect, true);
                case LogicParamNode.BranchType.Or:
                    return EditorTexture.Load(new AssemblyResourceProvider(Assembly.GetExecutingAssembly(), "Bolt.Addons.Community.Fundamentals.Editor", "Resources"), "Branch_Or.png", CreateTextureOptions.PixelPerfect, true);
                case LogicParamNode.BranchType.GreaterThan:
                    if (unit.AllowEquals)
                        return EditorTexture.Load(new AssemblyResourceProvider(Assembly.GetExecutingAssembly(), "Bolt.Addons.Community.Fundamentals.Editor", "Resources"), "Branch_Greater_Equal.png", CreateTextureOptions.PixelPerfect, true);
                    else
                        return EditorTexture.Load(new AssemblyResourceProvider(Assembly.GetExecutingAssembly(), "Bolt.Addons.Community.Fundamentals.Editor", "Resources"), "Branch_Greater.png", CreateTextureOptions.PixelPerfect, true);
                case LogicParamNode.BranchType.LessThan:
                    if (unit.AllowEquals)
                        return EditorTexture.Load(new AssemblyResourceProvider(Assembly.GetExecutingAssembly(), "Bolt.Addons.Community.Fundamentals.Editor", "Resources"), "Branch_Less_Equal.png", CreateTextureOptions.PixelPerfect, true);
                    else
                        return EditorTexture.Load(new AssemblyResourceProvider(Assembly.GetExecutingAssembly(), "Bolt.Addons.Community.Fundamentals.Editor", "Resources"), "Branch_Less.png", CreateTextureOptions.PixelPerfect, true);
                case LogicParamNode.BranchType.Equal:
                    return EditorTexture.Load(new AssemblyResourceProvider(Assembly.GetExecutingAssembly(), "Bolt.Addons.Community.Fundamentals.Editor", "Resources"), "Branch_Equal.png", CreateTextureOptions.PixelPerfect, true);
                default:
                    return EditorTexture.Load(new AssemblyResourceProvider(Assembly.GetExecutingAssembly(), "Bolt.Addons.Community.Fundamentals.Editor", "Resources"), "arrow_switch.png", CreateTextureOptions.PixelPerfect, true);
            }

        }

        protected override void DefinedPort(IUnitPort port, UnitPortDescription description)
        {
            base.DefinedPort(port, description);

            if (port == unit.exitTrue)
            {
                switch (unit.BranchingType)
                {
                    case LogicParamNode.BranchType.And:
                        description.label = "True";
                        description.summary = "Exit control flow if all inputs are true.";
                        break;
                    case LogicParamNode.BranchType.Or:
                        description.label = "True";
                        description.summary = "Exit control flow if any of the inputs are true.";
                        break;
                    case LogicParamNode.BranchType.GreaterThan:
                        description.label = "Greater";
                        description.summary = string.Format("Exit control flow if the first input is greater than {0}the second.", unit.AllowEquals ? "or equal to " : "");
                        break;
                    case LogicParamNode.BranchType.LessThan:
                        description.label = "Less";
                        description.summary = string.Format("Exit control flow if the first input is less than {0}the second.", unit.AllowEquals ? "or equal to " : "");
                        break;
                    case LogicParamNode.BranchType.Equal:
                        description.label = "Equal";
                        description.summary = "Exit control flow if all of the inputs are equal.";
                        break;
                    default:
                        break;
                }
            }

            if (port == unit.exitFalse)
            {
                switch (unit.BranchingType)
                {
                    case LogicParamNode.BranchType.And:
                        description.label = "False";
                        description.summary = "Exit control flow if one of the inputs is false.";
                        break;
                    case LogicParamNode.BranchType.Or:
                        description.label = "False";
                        description.summary = "Exit control flow if none of the inputs are true.";
                        break;
                    case LogicParamNode.BranchType.GreaterThan:
                        description.label = "Less";
                        description.summary = string.Format("Exit control flow if the first input is less than {0}the second.", unit.AllowEquals ? "" : "or equal to ");
                        break;
                    case LogicParamNode.BranchType.LessThan:
                        description.label = "Greater";
                        description.summary = string.Format("Exit control flow if the first input is greater than {0}the second.", unit.AllowEquals ? "" : "or equal to ");
                        break;
                    case LogicParamNode.BranchType.Equal:
                        description.label = "Not Equal";
                        description.summary = "Exit control flow if any of the inputs are not equal.";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}