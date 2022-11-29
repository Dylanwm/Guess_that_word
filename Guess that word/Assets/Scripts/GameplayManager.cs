using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class GameplayManager : MonoBehaviour
{
    public GameObject hangmanImage;                                                             //contains the current hangman image
    public Text guessWord;                                                                      //displays the word that needs to be guessed to the user
    public Text guessWordLength;                                                                //displays the length of the hidden word in number form
    public Text guessedLetter;                                                                  //displays the last letter the player guessed
    public Text whatTheWordWasLost;                                                             //displays what the word was when the game ends
    public Text whatTheWordWasWon;
    public string whatTheWordWas;
    public string[] wordList;                                                                   //contains various words that could be chosen at random
    public string hiddenWord;                                                                   //holds the word that needs to be guessed
    public char[] hiddenWordAsLetter;                                                           //contains the word as individual characters
    public string[] characters;                                                                 //the "hiddenWord" as characters
    public int hiddenWordLength;                                                                //holds a measurement of how many characters long the hidden word is
    public int correctGuessesAmount = 0;                                                        //contains how many correct guesses the player has made
    public int incorrectGuessesAmount = 0;                                                      //contains how many incorrect guesses the player has made
    public int gameoverGuessAmount = 7;                                                         //contains the amount of guesses the player has before GameOver
    public Sprite[] hangmanImages;                                                              //contains the hangman images
    public char previousGuessedLetter = ' ';                                                    //contains the previous letter input to prevent multiple guesses of the same key being made
    public string inputKey;
    public char inputtedKey;
    public bool isPlaying = false;
    public PanelManager panelManager;
    void Start()
    {
        wordList = new string[20];                                                              //creates a wordlist containing all possible hidden words
        wordList[0] = "ORANGE";                                         
        wordList[1] = "NAPTIME";
        wordList[2] = "TERGIVERSATION";
        wordList[3] = "HIPPOPOTAMUS";                                   
        wordList[4] = "HIPPOCAMPUS";                                    
        wordList[5] = "LUCKY";                                   
        wordList[6] = "DOG";                                            
        wordList[7] = "GAMESYSTEMS";
        wordList[8] = "WORCESTERSHIRE";
        wordList[9] = "ASSESSORS";
        wordList[10] = "STATISTICS";
        wordList[11] = "HANGMAN";
        wordList[12] = "INEPT";
        wordList[13] = "INAPT";
        wordList[14] = "PROGRAMMING";
        wordList[15] = "TAFE";
        wordList[16] = "DIFFICULT";
        wordList[17] = "PEPPER";
        wordList[18] = "SATELLITE";
        wordList[19] = "RANDOM";
        //wordList[20] = "HIPPOPOTOMONSTROSESQUIPPEDALIOPHOBIA";                                  //ironically the fear of long words
        //wordList[21] = "INCOMPREHENSIBILITY";
        SelectHiddenWord();
    }

    void OnGUI()                                                                                //runs when key or mouse interation is detected
    {
        Event e = Event.current;                                                                //saves the detected key press as the variable "e"

        if (Input.GetKey(KeyCode.Escape))                                                       //if the escape key is pressed
        {
            Application.Quit();                                                                 //close the game
        }

        else if (isPlaying == true)
        {
            if (Input.GetKey("space"))
            {
                panelManager.PanelChange(1);
            }
            else if (e.isKey)                                                                       //if a key was pressed
            {
                inputtedKey = Char.Parse(e.keyCode.ToString());                                     //converts the key pressed into a string and saves it in the variable "inputtedKey"
                if (inputtedKey != previousGuessedLetter)                                           //if the key pressed isn't the same as the previously pressed key
                {
                    previousGuessedLetter = inputtedKey;                                            //set the new previously pressed key as the currently pressed key
                    CheckGuess(inputtedKey);                                                        //run function below
                }
            }
        }
        
    }


    public void SelectHiddenWord()                                                              //this function selects a random word from the "wordList" and converts it into "_" and displays it to the player
    {
        

        correctGuessesAmount = 0;
        incorrectGuessesAmount = 0;
        UpdateHangmanImage(0);
        int randomNumber = UnityEngine.Random.Range(0, wordList.Length);                        //generates a random number between 0 and the length of the wordList
        whatTheWordWas = hiddenWord;
        hiddenWord = wordList[randomNumber];                                                    //saving the selected word from the word list into the variable "hiddenWord"
        whatTheWordWasLost.text = "The word was " + hiddenWord;
        whatTheWordWasWon.text = "The word was " + hiddenWord;
        hiddenWordLength = hiddenWord.Length;                                                   //saving the length of the selected word as a variable "hiddenWordLength"
        guessWord.text = hiddenWord;                                            
        guessWordLength.text = "Word length is " + hiddenWordLength.ToString();                 //sets up what is being displayed in the text box labeled "guessWordLength" 
                                                                                                //converts "hiddenWordLength" from an int to a string
        hiddenWordAsLetter = hiddenWord.ToCharArray();                                          //converts "hiddenWord" into an array of characters and saves it as variable "hiddenWordAsLetters"
        characters = new string[hiddenWordLength];                                              //creates a string the same length as the "hiddenWord" and saves it as characters
        guessWord.text = "";                                                                    //resets text box
        for (int i = 0; i < hiddenWordLength; i++)                                              //run as many times as the length of the "hiddenWord"
        {
            characters[i] = " _ ";                                                                //turn each character in "_"
            guessWord.text += characters[i];                                                    //display each character
        }
        whatTheWordWasLost.text = "The word was " + whatTheWordWas;
        whatTheWordWasWon.text = "The word was " + whatTheWordWas;
    }

    void CheckGuess(char letterGuessed)                                                         //checks if a letter is correct and if it is it updates the correct letter
    {
        bool guessedCorrect = false;                                                            //creates and sets a bool labelled "guessedCorrect" to false
        inputKey = letterGuessed.ToString();                                                    //converts the letter the player has guessed into a string and saves it as a variable
        guessedLetter.text = inputKey;                                                          //displays the players most recent guess in the text box labelled "guessedLetter"
        for (int i = 0; i < hiddenWordLength; i++)                                              //run as many times as the length of the "hiddenWord"
        {
            if (hiddenWordAsLetter[i] == letterGuessed)                                         //checks to see if the players guess matches any letters in the "hiddenWord"
            {
                guessedCorrect = true;                                                          //sets the bool to true, which runs the if statement below
                characters[i] = " " + inputKey + " ";                                           //changes the hidden characters in the string from "_" to letters
                correctGuessesAmount++;                                                         //increments the total correct guesses made by the player by 1
            }
        }

        if (guessedCorrect == true)                                                             //if the player has guessed right
        {
            guessWord.text = "";                                                                //resets text box
            for (int i = 0; i < hiddenWordLength; i++)
            {
                guessWord.text += characters[i];                                                //updates the text box labelled "guessWord" and displays the letters that were guessed correctly as letters
            }
        }
        else                                                                                    //if the player has guessed wrong
        {
            incorrectGuessesAmount++;                                                           //increment the total incorrect guesses the player has made
            UpdateHangmanImage(incorrectGuessesAmount);                                         //runs functions below using the amount of incorrect guesses the player has made
        }
        CheckWinLossState();                                                                    //runs the function below
    }

    void UpdateHangmanImage(int x)                                                              //updates hangman image matching the amount of incorrect guesses to the corresponding image
    {
        if (x < hangmanImages.Length)                                                           //if there is another hangman image
        {
            hangmanImage.GetComponent<Image>().sprite = hangmanImages[x];                       //update the hangman image with the new image
        }
    }
    void CheckWinLossState()                                                                    //function to check if the player has won/lost yet
    {
        if (correctGuessesAmount >= hiddenWordLength)
        {
            panelManager.PanelChange(3);
            SelectHiddenWord();
        }
        else if (incorrectGuessesAmount >= gameoverGuessAmount)
        {
            panelManager.PanelChange(4);
            SelectHiddenWord();
        }
    }
}
