using System.Collections.Generic;
using System.Linq;
using FabriciohodDev.DialogueSystem.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace FabriciohodDev.DialogueSystem.Editor
{
    public class GraphSaveUtility
    {
        private DialogueContainer containerCache;
        private DialogueGraphView targetGraphView;
        private List<Edge> edges => targetGraphView.edges.ToList();
        private List<DialogueNode> nodes => targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();

        public static GraphSaveUtility Instance(DialogueGraphView _targetGraphView)
        {
            return new GraphSaveUtility
            {
                targetGraphView = _targetGraphView
            };
        }

        public void SaveGraph(string fileName)
        {
            var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();
            
            if(!SaveNodes(dialogueContainer)) return;
            SaveExposedProperty(dialogueContainer);

            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                AssetDatabase.CreateFolder("Assets", "Resources");
            if (!AssetDatabase.IsValidFolder("Assets/Resources/DialogueSystem"))
                AssetDatabase.CreateFolder("Assets/Resources", "DialogueSystem");


            AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Resources/DialogueSystem/{fileName}.asset");
            AssetDatabase.SaveAssets();
        }

        private void SaveExposedProperty(DialogueContainer dialogueContainer)
        {
            dialogueContainer.exposedProperties.AddRange(targetGraphView.exposedProperties);
        }

        public void LoadGraph(string fileName)
        {
            containerCache = Resources.Load<DialogueContainer>($"DialogueSystem/{fileName}");

            if (containerCache is null)
            {
                EditorUtility.DisplayDialog("File Not Found", "Target Dialogue graph file does not exits!", "OK");
                return;
            }

            ClearGraph();
            CreateNodes();
            ConnectNodes();
            CreateExposedProperties();
        }

        private void CreateExposedProperties()
        {
            targetGraphView.ClearBlackboardAndExposedProperties();
            
            foreach (var exposedProperty in containerCache.exposedProperties)
            {
                targetGraphView.AddPropertyToBlackboard(exposedProperty);
            }
        }

        private bool SaveNodes(DialogueContainer dialogueContainer)
        {
            if (!edges.Any()) return false;

            var connectedPorts = edges.Where(x => x.input.node != null).ToArray();

            for (var i = 0; i < connectedPorts.Length; i++)
            {
                var outputNode = connectedPorts[i].output.node as DialogueNode;
                var inputNode = connectedPorts[i].input.node as DialogueNode;

                dialogueContainer.nodeLinks.Add(new NodeLinkData
                {
                    baseNodeGuid = outputNode.GUID,
                    portName = connectedPorts[i].output.portName,
                    targetNodeGuid = inputNode.GUID
                });
            }

            foreach (var dialogueNode in nodes.Where(node => !node.EntryPoint))
                dialogueContainer.dialogueNodeData.Add(new DialogueNodeData
                {
                    guid = dialogueNode.GUID,
                    dialogueText = dialogueNode.DialogueText,
                    position = dialogueNode.GetPosition().position
                });

            return true;
        }

        private void ClearGraph()
        {
            nodes.Find(x => x.EntryPoint).GUID = containerCache.nodeLinks[0].baseNodeGuid;

            foreach (var node in nodes)
            {
                if (node.EntryPoint) continue;

                edges.Where(x => x.input.node == node)
                    .ToList()
                    .ForEach(edge => targetGraphView.RemoveElement(edge));

                targetGraphView.RemoveElement(node);
            }
        }

        private void CreateNodes()
        {
            foreach (var nodeData in containerCache.dialogueNodeData)
            {
                var tempNode = targetGraphView.CreateDialogueNode(nodeData.dialogueText, Vector2.zero);
                tempNode.GUID = nodeData.guid;
                targetGraphView.AddElement(tempNode);

                var nodePorts = containerCache.nodeLinks.Where(x => x.baseNodeGuid == nodeData.guid).ToList();

                nodePorts.ForEach(x => targetGraphView.AddChoicePort(tempNode, x.portName));
            }
        }

        private void ConnectNodes()
        {
            for (var i = 0; i < nodes.Count; i++)
            {
                var connections = containerCache.nodeLinks.Where(x => x.baseNodeGuid == nodes[i].GUID).ToList();
                for (var j = 0; j < connections.Count; j++)
                {
                    var targetNodeGuid = connections[j].targetNodeGuid;
                    var targetNode = nodes.First(x => x.GUID == targetNodeGuid);

                    LinkNodes(nodes[i].outputContainer[j].Q<Port>(), (Port) targetNode.inputContainer[0]);

                    targetNode.SetPosition(new Rect(
                        containerCache.dialogueNodeData.First(x => x.guid == targetNodeGuid).position,
                        targetGraphView.defaultSize
                    ));
                }
            }
        }

        private void LinkNodes(Port output, Port input)
        {
            var tempEdge = new Edge
            {
                output = output,
                input = input
            };

            tempEdge?.input.Connect(tempEdge);
            tempEdge?.output.Connect(tempEdge);

            targetGraphView.Add(tempEdge);
        }
    }
}