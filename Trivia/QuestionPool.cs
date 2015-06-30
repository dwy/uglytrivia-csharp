namespace Trivia
{
    using System.Collections.Generic;
    using System.Linq;

    public class QuestionPool
    {
        private LinkedList<string> popQuestions = new LinkedList<string>();
        private LinkedList<string> scienceQuestions = new LinkedList<string>();
        private LinkedList<string> sportsQuestions = new LinkedList<string>();
        private LinkedList<string> rockQuestions = new LinkedList<string>();

        public QuestionPool()
        {
            this.InitQuestions();
        }

        private void InitQuestions()
        {
            for (int i = 0; i < 50; i++)
            {
                this.popQuestions.AddLast("Pop Question " + i);
                this.scienceQuestions.AddLast(("Science Question " + i));
                this.sportsQuestions.AddLast(("Sports Question " + i));
                this.rockQuestions.AddLast("Rock Question " + i);
            }
        }

        public string GetQuestion(QuestionCategory questionCategory)
        {
            var currentList = new LinkedList<string>();
            if (questionCategory == QuestionCategory.Pop)
            {
                currentList = this.popQuestions;
            }
            if (questionCategory == QuestionCategory.Science)
            {
                currentList = this.scienceQuestions;
            }
            if (questionCategory == QuestionCategory.Sports)
            {
                currentList = this.sportsQuestions;
            }
            if (questionCategory == QuestionCategory.Rock)
            {
                currentList = this.rockQuestions;
            }
            string question = currentList.First();
            currentList.RemoveFirst();
            return question;
        }
    }
}