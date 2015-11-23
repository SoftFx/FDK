using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Parser.Comon
{
    /// <summary>
    /// This class maps cleanly the non-space and comment characters
    /// This is useful to find 
    /// </summary>
    public class CleanupAstNodeStates : IEnumerable<ParseNode>
    {
        public List<ParseNode> Nodes { get; set; }
        public List<ParseNode> MappedNodes { get; private set; }

        public CleanupAstNodeStates(List<ParseNode> nodes)
        {
            Initialize(nodes);
            if (nodes.Count > 0)
            {
                var nodeWithParent = nodes.FirstOrDefault(node => node.Parent != null);
                if (nodeWithParent != null)
                    Parent = nodeWithParent.Parent;
            }
        }

        public CleanupAstNodeStates(ParseNode parent)
        {
            Parent = parent;
            Initialize(parent.Children);
        }

        void Initialize(List<ParseNode> nodes)
        {
            Nodes = nodes;
            MappingNotSpacesNodes = new Dictionary<int, int>();
            MappedNodes = new List<ParseNode>();

            MapNodes();
        }

        public override string ToString()
        {
            return string.Join("", MappedNodes.Select(it =>
                {
                    var tokenData = it;
                    return tokenData == null ? string.Empty : tokenData.Content;
                }));
        }


        public ParseNode Parent { get; set; }

        void MapNodes()
        {
            var pos = 0;
            if (Nodes == null)
                return;
            for (var i = 0; i < Nodes.Count; i++)
            {
                var astNode = Nodes[i];
                if (AddNodeIfSkipSpaces(astNode))
                {
                    MappingNotSpacesNodes[pos] = i;
                    pos++;
                    MappedNodes.Add(astNode);
                }
            }
            foreach (var node in Nodes.Where(node => node.Parent != null))
            {
                Parent = node.Parent;
                break;
            }
        }

        public static bool AddNodeIfSkipSpaces(ParseNode astNode)
        {
            var tokenKind = astNode.Token;
            var toAdd = (tokenKind != TokenKind.Space &&
                         tokenKind != TokenKind.Comment);
            return toAdd;
        }

        public Dictionary<int, int> MappingNotSpacesNodes { get; private set; }

        public int Count
        {
            get { return MappedNodes.Count; }
        }

        public void Remap()
        {
            MappingNotSpacesNodes.Clear();
            MappedNodes.Clear();
            MapNodes();
        }

        public void Insert(int index, ParseNode node)
        {
            node.Parent = Parent;
            var mappedIndex = MappingNotSpacesNodes[index];
            Nodes.Insert(mappedIndex, node);
        }

        public void InsertRange(int index, IEnumerable<ParseNode> node)
        {
            var nodesToBeAdded = node.ToArray().Reverse();
            foreach (var astNode in nodesToBeAdded)
            {
                Insert(index, astNode);
            }
            Remap();
        }

        public void RemoveAt(int index)
        {
            var mappedIndex = MappingNotSpacesNodes[index];
            Nodes.RemoveAt(mappedIndex);
            Remap();
        }

        public void RemoveRange(int startIndex, int endIndex)
        {
            for (var removeRange = endIndex; removeRange >= startIndex; removeRange--)
            {
                RemoveAt(removeRange);
            }
        }

        public ParseNode ReduceRangeOfTokensAsParent(int startPosition, int endPosition, RuleKind ruleKind)
        {
            var realStartPos = MappingNotSpacesNodes[startPosition];
            var realEndPos = MappingNotSpacesNodes[endPosition];
            var childSequence = new ParseNode(ruleKind)
                {
                    Parent = Nodes[startPosition].Parent
                };
            for (var i = realStartPos; i <= realEndPos; i++)
            {
                childSequence.Add(Nodes[i]);
            }
            Nodes.RemoveRange(realStartPos, realEndPos - realStartPos + 1);
            Nodes.Insert(realStartPos, childSequence);
            Remap();
            return childSequence;
        }

        #region List over mapped node

        public ParseNode this[int index]
        {
            get { return MappedNodes[index]; }
        }

        public int IndexOf(ParseNode node)
        {
            return MappedNodes.IndexOf(node);
        }

        public IEnumerator<ParseNode> GetEnumerator()
        {
            return MappedNodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}