using System.Collections.Generic;

namespace ExampleDialog.Model
{
    public class ModelDialogStep
    {
        public int id;
        public List<ModelPhraseNPC> npc_phrases;
        public List<ModelPhrasePlayer> answers;
    }
}