// KEY WORD USED IN ALOT OF MY COMMENTS IS ANITIALIZE
// ANITIALIZE REFERS TO THE ASSIGNING OF A VARIABLE
// OR THE SETTING UP OF A OBJECT SO THAT IT IS READY FOR USE

// NAMESPACE AND IMPORTS
using System; // I imported system for fundamental classes and base classes
using System.Collections.Generic; // I imported the Collections.Generic class so that I may use the dictionary to store questions and answers
using System.Windows.Forms; // I imported the System.Windows.Forms class so that we can create a windows based application
using System.Media; // For sound handling
using System.Windows.Forms.Design; // I make use of the System.Media package to provide me with classes for sound handling

namespace CyberSecurityApp // The namespace defines the scope for my application grouping all the classes I created
{
    // CLASS AND FORM DEFINITION
    public partial class Form1 : Form // This partial class makes use of inheritance from the Form import to generate a window for my application to run in
    {
        // VARIABLES
        private Dictionary<string, AnswerInfo> questionsAndAnswers; // I created this directory to store all my questions and answers for the application
        private string userName; // This variable stores the name the user inputs when the application starts
        private string currentText; // I make use of this variable to store the current text/answer that will be provided to the user
        private int currentIndex; // I make use of this variable to keep track of the current character being typed in the answer box
        private System.Windows.Forms.Timer typingTimer; // I created this timer object to control the speed at which my answer is typed

        /*This is a class i created to hold the key word and the answer that goes with it
         after research i chose to make use of data encaptulation as this makes the handling of the data easyer
        by encapulating the keyword and its asociating answer in a single object handling becomes easyer and i am less likely to encounter any errors*/
        public class AnswerInfo
        {
            public string Text { get; set; } // this line is used to get and store the answer text from the textfile
            public string Keyword { get; set; } // this line is used to store my keyword

            /*This constructor is called when an instance of my answer info class is created
             this constructor makes use of 2 parameters these paramaters are used to initialize the properties of the class*/
            public AnswerInfo(string text, string keyword)
            {
                Text = text;
                Keyword = keyword;
            }
        }

       
        public Form1() // This method will be called when an instance of the form is created
        {
            InitializeComponent(); // This initializes all the components in the designer
            userName = askUsername("Please enter your name", "User name "); // This will display the dialog to ask the user for their name
            voiceGreeting(); // This will call the method to play the greeting message audio
            InitializeLibrary(); // This will ensure that the questions and their answers load in properly when the application starts
            InitializeTypingEffect(); // This will make sure that the timer is properly set up when the application starts

            // Below is a greeting message that will be shown to the user when the program starts 
            string greetingMessage = $"{userName}!\n\n" +
                "Weclome to CyberSavvy.\n" +
                "I am a cyber security bot. My purpose is to help you expand your cyber security knowladge " +
                " and keep your files and passwords safe.\n\n" +
                "Please note that i am in my development phase and as such i have a limited database as of right now\n\n" +
                " I am able to answer 22 cyber security related questions i look forward to helping you.";

            StartTypingEffect(greetingMessage);
        }

        // VOICE GREETING METHOD 
        private void voiceGreeting() // This method will load and play the sound file for the greeting
        {
            try
            {
                // The line below creates a sound player object called greeting our sound file is stored in this object
                using (SoundPlayer greeting = new("recorded greeting.wav")) // Sound Player is a class in the media package I imported that plays sound files that are in wav format
                {
                    greeting.Play(); // This line plays the sound file stored in the soundplayer instance called greeting
                }
            }
            catch (Exception ex) // I use the try catch block to handle any exceptions that may occur while trying to play the greeting.
            // Try catch blocks are often used when handling external files like text files, video files or sound files.
            {
               
            }
        }

        /*The below method initializes the library of questions and answers
         my keywords and answers are bothe stored in a text file i had the library in my code at first but it was alor of lines and looked un neat*/
        private void InitializeLibrary()
        {
            questionsAndAnswers = new Dictionary<string, AnswerInfo>();

            string[] lines = System.IO.File.ReadAllLines("library.txt"); // Read lines from the file

            foreach (string line in lines)
            {
                // This line splits the line retrieved into a question and an answer 
                string[] parts = line.Split(new[] { ',' }, 2); // Here I also tell the program to only split at the first comma it finds in the line
                if (parts.Length == 2)
                {
                    string question = parts[0].Trim().ToLower(); // Here I set all the retrieved text to lowercase for easier comparison (could say it is error handling)
                    string answer = parts[1].Trim();

                    questionsAndAnswers[question] = new AnswerInfo(answer, question);
                }
            }
        }

        /*This method is called when the ask button is clicked
         this is also a event listener the program uses the method whenever the button is clicked (button event)*/
        private void naswerButton_Click(object sender, EventArgs e)
        {   
            /*This line stores the user input in a variable called userInput
             the Trim() function is used to remove white spaces in the text 
            the text is then set to lower case for easyer handling and comparrison*/
            string userInput = questionBox.Text.Trim().ToLower(); 

            /* any key words found are stored in this arraylist i had to make use of a arraylist for this as 
             i have questions that may contain 2 key words and one of the key words is the same as another questions key word 
            i found that it created a problem since i have things like a keyword malware and then i have malware analysis 
            and if malware analysis was typed it just responded with the answer for malware so now i store all related keywords and answers in this list */
            List<string> foundAnswers = new List<string>(); 

            /* here i now first take the recodnised keywords and check for exact matches for example if malware analysis was typed it will recodnise malware analysis first  */
            if (questionsAndAnswers.ContainsKey(userInput))
            {
                //this line tells the program to store all matched keywords and answers in the array list created
                foundAnswers.Add(questionsAndAnswers[userInput].Text); 
                // this line looks for a keyword match in my audio folder to find the corresponding answer audio the audio file is then stored in the audiofilepath variable
                string audioFilePath = $"audio/{userInput}.wav";
                // here i call the play answer audio method to play the audio stored in the audiofilepath variable
                PlayAnswerAudio(audioFilePath);
            }
            else
            {
                // here we use a for loop to itterate through the library to see if a single keyword can be found since no exact matches were found */
                foreach (var answers in questionsAndAnswers)
                {
                   /* this if statement block of code tells the program if a keyword is found
                    add it to the list then the code looks for a keyword match in the audio file
                   the audio is stored in the audiofilepath variable and then played along with the answer*/
                    if (userInput.Contains(answers.Key))
                    {
                        foundAnswers.Add(answers.Value.Text);
                        string audioFilePath = $"audio/{answers.Value.Keyword}.wav"; // here we also spesifie where to look for the audion file we define the file path
                        PlayAnswerAudio(audioFilePath);// yhis line plays the found audio file 
                    }
                }
            }

            /*
              this is the block of code that actually executes when a answer and a audio file was found 
            if a answer was found but no audio file then the program will appologise for not having the audio file but still give the text answer*/ 
            if (foundAnswers.Count > 0) 
            {
                // here we make use of string concatination to personalise the answer by adding the user name. 
                string combinedAnswer = $"{userName},:\n" + string.Join("\n", foundAnswers);
                StartTypingEffect(combinedAnswer);
            }
            else 
            {    
                // if no key words are found the application responds with this message aswell as a audio response.
                StartTypingEffect($"{userName}, Sorry but i don't seem to have any information related to your question i hope to " +
                    $"expand my database in the future but for now is there anything else i may help you with");
            }using(SoundPlayer sorry = new("no answer.wav"))
            {
                sorry.Play();
            }
        }

        // This clear button is used to clear any input in the question field 
        private void clearButton_Click(object sender, EventArgs e)
        {
            // This line in the method tells the program to clear the text in the questionBox
            questionBox.Text = null;
        }

        // This clear button is used to clear any input in the answer field 
        private void clearButton2_Click(object sender, EventArgs e)
        {
            // This line in the method tells the program to clear the text in the answerBox
            // The addition of the typingTimer.Stop is so that if the clear button is pressed while the program is still typing it will stop typing
            typingTimer.Stop();
            answerBox.Text = null;
        }

        // This method is used to produce a popup to ask the user for their name when the application is started
        private string askUsername(string prompt, string title)
        {
            MessageBox.Show("Welcome to the CyberSecurity Bot!\n\n" +
                "This app provides clear answers to common cybersecurity questions.\n\n" +
                "Type your question about topics like phishing, firewalls, or encryption, and let's enhance your cybersecurity knowledge!\n\n" +
                "Please enter your name to get started.",
                "Introduction", MessageBoxButtons.OK, MessageBoxIcon.Information);
            /* Here we create a new instance of a form for the popup
            We set the size by defining width and height
            We also set the start position of the form in the center of our screen */
            Form name = new Form()
            {
                Width = 400,
                Height = 200,
                StartPosition = FormStartPosition.CenterScreen
            };

            /* Here I create a label for our popup and set the position 20 pixels from the left and 30 pixels from the top
            I also set the auto size function to true so that we can add text to the box without concern of space */
            Label nameLabel = new Label()
            {
                Top = 30,
                Left = 20,
                Text = prompt,
                AutoSize = true
            };

            /* Here I create a new text box this textbox will be used by the user to enter their name
            I define the position of the box as well as the width */
            TextBox nameBox = new TextBox()
            {
                Left = 50,
                Top = 60,
                Width = 300
            };

            /* Here I create a button that the user can press to confirm the entry of his/her name 
            I set the text that will display on the button I also define its size and position */
            Button enterName = new Button()
            {
                Text = "ENTER",
                Left = 250,
                Width = 100,
                Top = 100,
                Height = 40
            };

            /* This is an event listener for the enter button
            The event listener has 2 functions 
            Function 1 is if the user has not provided us with a name then I display an error message telling the user to enter a name 
            Function 2 is if the user has entered their name and clicked the button, the name is stored in the username variable */
            enterName.Click += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(nameBox.Text))
                {
                    MessageBox.Show("Sorry you did not enter your name you must enter your name to proceed!", "NO NAME ENTERED", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                }
                else
                // If a username is entered and stored, the dialog box closes
                { name.Close(); }
            };

            // Here I add all the created components to my new form
            name.Controls.Add(nameLabel);
            name.Controls.Add(nameBox);
            name.Controls.Add(enterName);
            name.Text = title;
            name.ShowDialog();

            // This ensures that the event listener returns with the username
            return nameBox.Text;
        }

        // This event listener is for the close button and does what you would expect it terminates the program
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // This method is responsible for setting up the timer for our typing effect in the output
        private void InitializeTypingEffect()
        {
            // Here I create an instance of the timer
            typingTimer = new System.Windows.Forms.Timer();
            // This line sets the interval in milliseconds for the timer so every 50 ms it will type a letter
            typingTimer.Interval = 50;
            // This line makes use of the timer tick effect to handle the typing effect
            typingTimer.Tick += TypingTimer_Tick;
        }

        // This method prepares the answer to be typed one character at a time
        private void StartTypingEffect(string text)
        {
            currentText = text; // This variable stores the answer that will be typed
            currentIndex = 0; // This sets our index to 0 to ensure the answer is typed from the beginning
            answerBox.Clear(); // This line clears any previous answer so that there is no confusion with our answer
            typingTimer.Start(); // This line tells the program to start typing the answer
        }

        // This method is called every time the timer ticks
        private void TypingTimer_Tick(object sender, EventArgs e)
        {
            if (currentIndex < currentText.Length)
            {
                // This line makes sure that as long as there are characters left in our currentText variable, the program will continue typing
                answerBox.AppendText(currentText[currentIndex].ToString());
                // This line increments our index by one and prevents an infinite loop
                currentIndex++;
            }
            else
            {
                typingTimer.Stop(); // This line stops the timer and the typing when all characters have been typed in the answerBox
            }
        }

        /* i created a seperate method to play the audio responses on questions
          just to ensure that there is no error between this and the audio for the greeting*/
        private void PlayAnswerAudio(string audioFilePath)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(audioFilePath))
                {
                    player.Play(); 
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Error playing audio: {ex.Message}", "Im sorry i dont seem to have a audio file for this " +
                    "i appologise for any incovinience", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}