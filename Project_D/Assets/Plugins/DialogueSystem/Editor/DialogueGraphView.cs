using System;
using System.Collections.Generic;
using System.Linq;
using FabriciohodDev.DialogueSystem.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace FabriciohodDev.DialogueSystem.Editor
{
    public class DialogueGraphView : GraphView
    {
        public readonly Vector2 defaultSize = new Vector2(150, 200);
        public NodeSearchWindow searchWindow;
        public List<ExposedProperty> exposedProperties = new List<ExposedProperty>();
        public Blackboard blackboard;

        public DialogueGraphView(EditorWindow editorWindow)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("DialogueGraph"));
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();

            AddElement(GenerateEntryPointNode());
            
            AddSearchWindow(editorWindow);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();

            ports.ForEach(port =>
            {
                if (startPort != port && startPort.node != port.node) compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }

        private Port GeneratePort(DialogueNode node, Direction portDirection,
            Port.Capacity capacity = Port.Capacity.Single)
        {
            return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
        }

        private DialogueNode GenerateEntryPointNode()
        {
            var node = new DialogueNode
            {
                title = "Start",
                GUID = Guid.NewGuid().ToString(),
                DialogueText = "Entry Point",
                EntryPoint = true
            };

            var generatePort = GeneratePort(node, Direction.Output);
            generatePort.portName = "Next";
            node.outputContainer.Add(generatePort);

            node.capabilities &= ~Capabilities.Movable;
            node.capabilities &= ~Capabilities.Deletable;

            node.RefreshExpandedState();
            node.RefreshPorts();

            node.SetPosition(new Rect(100, 200, 100, 150));

            return node;
        }

        public void CreateNode(string nodeName, Vector2 position)
        {
            AddElement(CreateDialogueNode(nodeName, position));
        }

        public DialogueNode CreateDialogueNode(string nodeName, Vector2 position)
        {
            var dialogueNode = new DialogueNode
            {
                title = nodeName,
                DialogueText = nodeName,
                GUID = Guid.NewGuid().ToString()
            };

            var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "Input";
            dialogueNode.inputContainer.Add(inputPort);

            dialogueNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

            var button = new Button(() => { AddChoicePort(dialogueNode); });
            dialogueNode.titleContainer.Add(button);
            button.text = "New Choice";

            var textField = new TextField(string.Empty);
            textField.RegisterValueChangedCallback(evt =>
            {
                dialogueNode.DialogueText = evt.newValue;
                dialogueNode.title = evt.newValue;
            });
            textField.SetValueWithoutNotify(dialogueNode.title);
            dialogueNode.mainContainer.Add(textField);

            dialogueNode.RefreshPorts();
            dialogueNode.RefreshExpandedState();
            dialogueNode.SetPosition(new Rect(position, defaultSize));

            return dialogueNode;
        }

        public void AddChoicePort(DialogueNode dialogueNode, string overridenPortName = "")
        {
            var generatedPort = GeneratePort(dialogueNode, Direction.Output);

            var oldLabel = generatedPort.contentContainer.Q<Label>("type");
            generatedPort.contentContainer.Remove(oldLabel);

            var outputPortCount = dialogueNode.outputContainer.Query("connector").ToList().Count;

            var choicePortName = string.IsNullOrEmpty(overridenPortName)
                ? $"Choice {outputPortCount + 1}"
                : overridenPortName;

            var textField = new TextField
            {
                name = string.Empty,
                value = choicePortName
            };
            textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
            generatedPort.contentContainer.Add(new Label("  "));
            generatedPort.contentContainer.Add(textField);

            var deleteButton = new Button(() => RemovePort(dialogueNode, generatedPort))
            {
                text = "X"
            };
            generatedPort.contentContainer.Add(deleteButton);

            generatedPort.portName = choicePortName;
            dialogueNode.outputContainer.Add(generatedPort);
            dialogueNode.RefreshPorts();
            dialogueNode.RefreshExpandedState();
        }

        private void RemovePort(Node dialogueNode, Port generatedPort)
        {
            var targetEdge = edges.ToList().Where(x =>
                x.output.portName == generatedPort.portName && x.output.node == generatedPort.node);

            if (!targetEdge.Any()) return;

            var edge = targetEdge.First();
            edge.input.Disconnect(edge);
            RemoveElement(targetEdge.First());

            dialogueNode.outputContainer.Remove(generatedPort);
            dialogueNode.RefreshPorts();
            dialogueNode.RefreshExpandedState();
        }

        private void AddSearchWindow(EditorWindow editorWindow)
        {
            searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
            searchWindow.Init(editorWindow, this);
            nodeCreationRequest = context =>
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);

        }

        public void AddPropertyToBlackboard(ExposedProperty exposedProperty)
        {
            var localPropertyName = exposedProperty.propertyName;
            var localPropertyValue = exposedProperty.propertyValue;
            while (exposedProperties.Any(x => x.propertyName == localPropertyName))
            {
                localPropertyName = $"{localPropertyName}(1)";
            }
            
            var property = new ExposedProperty();

            property.propertyName = localPropertyName;
            property.propertyValue = localPropertyValue;
            
            exposedProperties.Add(exposedProperty);

            var container = new VisualElement();
            var blackboardField = new BlackboardField
            {
                text = property.propertyName,
                typeText = "string property",
            };

            var propertyValueTextField = new TextField("Value: ")
            {
                value = localPropertyValue
            };
            
            propertyValueTextField.RegisterValueChangedCallback(evt =>
            {
                var changingPropertyIndex = exposedProperties.FindIndex(x => x.propertyName == property.propertyName);
                exposedProperties[changingPropertyIndex].propertyValue = evt.newValue;
            });
            
            container.Add(propertyValueTextField);
            var blackboardValueRow = new BlackboardRow(blackboardField, propertyValueTextField);
            container.Add(blackboardValueRow);
            
            container.Add(blackboardField);
            blackboard.Add(container);
        }

        public void ClearBlackboardAndExposedProperties()
        {
            exposedProperties.Clear();
            blackboard.Clear();
        }
    }
}