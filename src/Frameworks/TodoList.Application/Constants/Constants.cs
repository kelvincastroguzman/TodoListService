using Microsoft.Identity.Client;

namespace TodoList.Application.Constants
{
    internal class Constants
    {
        public struct Validations
        {
            public struct TodoItem
            {
                public const int MAX_TITLE_LENGTH = 50;
                public const int MAX_DESCRIPTION_LENGTH = 100;

                public struct Progression
                {
                    public const int MAX_PERCENT = 100;
                    public const int MIN_PERCENT = 1;
                    public const int PERCENT_NOT_ALLOWED_FOR_MODIFICATIONS = 50;
                }
            }
        }
    }
}
