﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuaSTGEditorSharp.EditorData;
using LuaSTGEditorSharp.EditorData.Document;
using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;

namespace LuaSTGEditorSharp.EditorData.Node.Object
{
    [Serializable, NodeIcon("playerprotect.png")]
    [RequireAncestor(typeof(CodeAlikeTypes))]
    [LeafNode]
    [RCInvoke(0)]
    public class PlayerProtect : TreeNode
    {
        [JsonConstructor]
        private PlayerProtect() : base() { }

        public PlayerProtect(DocumentData workSpaceData)
            : this(workSpaceData, "120") { }

        public PlayerProtect(DocumentData workSpaceData, string code)
            : base(workSpaceData)
        {
            Frames = code;
            //attributes.Add(new AttrItem("Time", code, this, "yield"));
        }

        [JsonIgnore, NodeAttribute]
        public string Frames
        {
            get => DoubleCheckAttr(0).attrInput;
            set => DoubleCheckAttr(0).attrInput = value;
        }

        public override IEnumerable<string> ToLua(int spacing)
        {
            string sp = Indent(spacing);
            yield return sp + "player.protect = " + Macrolize(0) + "\n";
        }
        
        public override IEnumerable<Tuple<int, TreeNode>> GetLines()
        {
            yield return new Tuple<int, TreeNode>(1, this);
        }

        public override string ToString()
        {
            return "Set player invulnerability to last \"" + attributes[0].AttrInput + "\" frames";
        }

        public override object Clone()
        {
            var n = new PlayerProtect(parentWorkSpace);
            n.DeepCopyFrom(this);
            return n;
        }
    }
}
