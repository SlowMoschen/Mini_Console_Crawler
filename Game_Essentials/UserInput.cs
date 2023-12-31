
/**
* This class handles all user input and validation
*
* @param string message - The message to be displayed to the user
* @param string[] options - The options the user can choose from
* @param string[] prices - The prices of the options - optional
* @return string - The user's choice
*/


namespace UserInput 
{
    public class InputHandler {

        public static string getChoice(string message, string[] options, string[] prices = null)
        {
            // Print Message or Question
            Console.WriteLine();
            Console.WriteLine(" " + message);

            // Print options
            for (int i = 0; i < options.Length; i++)
            {
                int number = i + 1;
                Console.WriteLine(" " + number + ". " + options?[i] + (prices != null ? " - " + prices[i] : ""));
            }

            Console.WriteLine();

            // Get input
            string? input = Console.ReadLine();

            // Check if input is a number
            if (int.TryParse(input, out int choiceNumber))
            {
                // Check if the number is within the valid range
                if (choiceNumber >= 1 && choiceNumber <= options.Length)
                {
                    return options[choiceNumber - 1];
                }
            }
            else
            {
                // Check if input is found in options
                // If so, return index of input
                if (options.Contains(input))
                {
                    return input;
                }
            }

            invalidInput(options);
            return getChoice(message, options);
        }

        public static int invalidInput (string[] options) {
            Console.WriteLine();
            Console.WriteLine(" Invalid Input, please enter one of the following: ");
            for (int i = 0; i < options.Length; i++) {
                Console.WriteLine(" " + options[i]);
            }
            return 0;
        }
    }
}