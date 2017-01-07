using ExampleDialog.Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace ExampleDialog.Controller
{
    class ControllerDialog
    {

        private ModelDialog dialog;
        private ModelDialogStep currentDialogStep;
        private int selectedAnswerIndex;
        public Dictionary<int, int?> DialogHistory;

        public int SelectedAnswerIndex { get { return selectedAnswerIndex; } }

        public int NPCPhrasesNum
        {
            get
            {
                return currentDialogStep == null || currentDialogStep.npc_phrases == null
                    ? 0
                    : currentDialogStep.npc_phrases.Count;
            }
        }

        public int AnswersNum
        {
            get
            {
                return currentDialogStep == null || currentDialogStep.answers == null
                        ? 0
                        : currentDialogStep.answers.Count;
            }
        }

        public void init(string file)
        {
            string json = readJson(file);
            if (json == null) return;
            dialog = JsonConvert.DeserializeObject<ModelDialog>(json);
            currentDialogStep = firstDialogStep();
            DialogHistory = new Dictionary<int, int?>();
        }

        private ModelDialogStep firstDialogStep()
        {
            if (dialog == null || dialog.phrases == null) return null;
            foreach (ModelDialogStep item in dialog.phrases)
                if (item.id == 0)
                    return item;
            return null;
        }

        internal string answerMessage(int i)
        {
            return currentDialogStep == null || currentDialogStep.answers == null || currentDialogStep.answers.Count <= i
                ? null
                : currentDialogStep.answers[i].message;
        }

        private string readJson(string file)
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return File.ReadAllText(path + file);
        }

        internal string npcPhrase(int i)
        {
            return currentDialogStep == null
                || currentDialogStep.npc_phrases == null
                || currentDialogStep.npc_phrases.Count <= i
                ? null
                : currentDialogStep.npc_phrases[i].message;
        }

        internal int? npcID(int i)
        {
            return currentDialogStep == null
                || currentDialogStep.npc_phrases == null
                || currentDialogStep.npc_phrases.Count <= i
                ? null
                : (int?)currentDialogStep.npc_phrases[i].npc_id;
        }

        internal void nextSelectedAnswerIndex()
        {
            if (AnswersNum == 0) return;
            ++selectedAnswerIndex;
            selectedAnswerIndex %= AnswersNum;
        }

        internal void previousSelectedAnswerIndex()
        {
            if (AnswersNum == 0) return;
            selectedAnswerIndex = (AnswersNum + selectedAnswerIndex - 1) % AnswersNum;
        }

        internal void answer(int i)
        {
            selectedAnswerIndex = i;
            answer();
        }

        internal void answer()
        {
            if (AnswersNum == 0)
            {
                currentDialogStep = null; //if answers is null, exit the dialog
                return;
            }

            if (AnswersNum <= selectedAnswerIndex)
                return;

            int? next_id = currentDialogStep.answers[selectedAnswerIndex].next_id;
            selectedAnswerIndex = 0;

            DialogHistory.Add(currentDialogStep.id, next_id);

            if (!next_id.HasValue)
            {
                currentDialogStep = null; //if no next id, exit the dialog
                return;
            }

            foreach (var step in dialog.phrases)
                if (step.id == next_id.Value)
                {
                    currentDialogStep = step;
                    break;
                }
        }
    }
}
