using System.Linq;
using FabriciohodDev.DialogueSystem.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace FabriciohodDev.DialogueSystem.Editor
{
    public class DialogueGraph : EditorWindow
    {
        private string fileName = "New Narrative";
        private DialogueGraphView graphView;

        private void OnEnable()
        {
            ConstructGraphView();
            GenerateToolbar();
            GenerateBlackBoard();
        }

        private void GenerateBlackBoard()
        {
            var blackBoard = new Blackboard(graphView);
            blackBoard.Add(new BlackboardSection{title = "Exposed Properties"});
            blackBoard.addItemRequested = _blackboard =>
            {
                graphView.AddPropertyToBlackboard(new ExposedProperty());
            };
            blackBoard.editTextRequested = (blackboard, element, newValue) =>
            {
                var oldPropertyName = ((BlackboardField) element).text;
                if (graphView.exposedProperties.Any(x => x.propertyName == newValue))
                {
                    EditorUtility.DisplayDialog("Error", "This property name already exists, plaese chose another one!",
                        "OK");
                    return;
                }

                var propertyIndex = graphView.exposedProperties.FindIndex(x => x.propertyName == oldPropertyName);

                graphView.exposedProperties[propertyIndex].propertyName = newValue;

                ((BlackboardField) element).text = newValue;
            };
            
            graphView.blackboard = blackBoard;
            blackBoard.SetPosition(new Rect(10, 30, 200, 300));
            
            graphView.Add(blackBoard);
        }

        private void OnDisable()
        {
            rootVisualElement.Remove(graphView);
        }

        [MenuItem("Graph/Dialogue Graph")]
        public static void OpenDialogueGraphWindow()
        {
            var window = GetWindow<DialogueGraph>();

            window.titleContent = new GUIContent("Dialogue Graph");
        }

        private void ConstructGraphView()
        {
            graphView = new DialogueGraphView(this)
            {
                name = "Dialogue Graph"
                
            };

            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        private void GenerateToolbar()
        {
            var toolBar = new Toolbar();

            var fileNameTextField = new TextField("File Name: ");
            fileNameTextField.SetValueWithoutNotify(fileName);
            fileNameTextField.MarkDirtyRepaint();
            fileNameTextField.RegisterValueChangedCallback(evt => fileName = evt.newValue);
            toolBar.Add(fileNameTextField);

            toolBar.Add(new Button(() => RequestDataOperation(true)) {text = "Save Data"});
            toolBar.Add(new Button(() => RequestDataOperation(false)) {text = "Load Data"});

            rootVisualElement.Add(toolBar);
        }

        private void RequestDataOperation(bool save)
        {
            if (string.IsNullOrEmpty(fileName))
                EditorUtility.DisplayDialog("Invalid file name!", "Please enter a valid file name.", "OK");

            var saveUtility = GraphSaveUtility.Instance(graphView);

            if (save)
                saveUtility.SaveGraph(fileName);
            else
                saveUtility.LoadGraph(fileName);
        }
    }
}