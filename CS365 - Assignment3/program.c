#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/wait.h>
#include <sys/types.h>
#include <string.h>
#include <errno.h>
#include <ctype.h>

#define READ 0
#define WRITE 1

const char * PIPE_DELIM = "|";
const char * SPACE_DELIM = " ";
#define TRUE 1
#define FALSE 0

typedef int Bool;

struct Pipe
{
    int fd[2];
};

typedef struct Pipe Pipe;

/*
* Split up a string by a delimiter and store each substring in the output array
* @param command The command that is going to split
* @param delimiter The delimiter to sacn for
* @param commands The array with the split substrings
* @return size The number of command substrings (not including NULL)
*/
size_t split(const char* command, const char* delimiter, char* commands[16])
{
    size_t size = 0;
    char *tokens;

    char *command_copy = strdup(command);

    tokens = strtok(command_copy, delimiter);

    while(tokens != NULL)
    {
        commands[size++] = strdup(tokens);
        tokens = strtok(NULL, delimiter);
    }
    commands[size] = NULL;
    return size;
}

/*
* Remove quotations around txt files if needed
* @param string The string to remove the quotations from
* @return returnString The string without quotations 
*/
char* removeQuotes(char* string)
{
    int currentLength = strlen(string);
    int newLength = currentLength - 2;
    char* end;
    char* returnString;
    returnString = malloc(sizeof(char) * newLength);

    int j = 0;

    for(int i = 0; i < currentLength; i++)
    {
        if(string[i] == '"')
        {
            i++;
        }
        else
        {
            returnString[j] = string[i];
            j++;
        }
    }
    
    returnString[j] = '\0';
    return (returnString);
}

/*
* Remove ">" redirect symbol and file name from string and redirect output to the file within string
* @param string The string to remove the ">" from
* @return returnString The string without the redirection symbol or file name
*/
char* removeRedirect(char* string)
{
    int currentLength = strlen(string);
    int newLength;
    char* end = strchr(string, '>');
    int characterIndex = (end - string);
    char* returnString;

    char* file;
    int fileEndIndex;
    int fileSize;
    int fileStartIndex = characterIndex + 2;

    int i;
    int j = fileStartIndex;

    for(i = fileStartIndex; i < currentLength; i++)
    {
        if(isspace(string[i]))
        {
            break;
        }
        j++;
    }
    if(j == currentLength)
    {
        j = j - 1;
    }

    fileEndIndex = j;
    fileSize = fileEndIndex - fileStartIndex;

    file = malloc(sizeof(char) * (fileSize + 1));
    j = 0;
    for(i = fileStartIndex; i <= fileEndIndex; i++)
    {
        file[j] = string[i];
        j++;
    }

    freopen(file, "w+", stdout);

    newLength = characterIndex - 1;
    returnString = malloc(sizeof(char) * newLength);
    
    for(i = 0; i < newLength; i++)
    {
        returnString[i] = string[i];
    }
    
    returnString[i] = '\0';
    return (returnString);
}

/*
* Trim leading and trailing white space in a string
* @param string The string to trim
* @return returnString The new string without leading and trailing white spaces)
*/
char* trim(char* string) {
    while(isspace(*string))
    {
        string++;
    }

    int currentLength = strlen(string);
    int i = currentLength - 1;
    int newLength;
    char* end;
    char* returnString;

    while(i > 0)
    {
        if(isspace(string[i]))
        {
            i--;
        }
        else
        {
            end = &string[i];
            if(i + 1 == currentLength)
            {
                return string;
            }
            break;
        }
    }

    newLength = i + 1;
    returnString = malloc(sizeof(char) * newLength);
    
    for(i = 0; i < newLength; i++)
    {
        returnString[i] = string[i];
    }
    
    returnString[i] = '\0';
    return (returnString);
}

/*
* Execute the commands and communicate information between pipes
* @param i Current index in commands array
* @param command The current command to execute
* @param redirect_input True or false if input needs to be redirected
* @param redirect_output True or false if the output needs to be redirected
* @param pipes The pipes array that the commands need to communicate between
*/
void execute(size_t i, const char *command, Bool redirect_input, Bool redirect_output, Pipe* pipes)
{
    char *commands[16];

    size_t number_of_commands = split(command, SPACE_DELIM, commands);

    for(int i = 0; i < number_of_commands; i++)
    {

        if(strchr(commands[i], '"') != NULL)
        {
            commands[i] = removeQuotes(commands[i]);
        }
    }
    
    if((fork()) == 0) //child!
    {
        if(redirect_output == TRUE && redirect_input != TRUE)
        {
            close(pipes[i].fd[READ]);
            dup2(pipes[i].fd[WRITE], STDOUT_FILENO);
            close(pipes[i].fd[WRITE]);
        }

        if(redirect_output == TRUE && redirect_input == TRUE)
        {
            close(pipes[i - 1].fd[WRITE]);
            dup2(pipes[i - 1].fd[READ], STDIN_FILENO);
            close(pipes[i - 1].fd[READ]);

            close(pipes[i].fd[READ]);
            dup2(pipes[i].fd[WRITE], STDOUT_FILENO);
            close(pipes[i].fd[WRITE]);
        } 
            
        if(redirect_input == TRUE && redirect_output != TRUE)
        {
            close(pipes[i - 1].fd[WRITE]);
            dup2(pipes[i - 1].fd[READ], STDIN_FILENO);
            close(pipes[i - 1].fd[READ]);
        }

        execvp(commands[0], commands);
        perror("Exec failed. Should never get here");
        exit(EXIT_FAILURE);  
        }
        else //parent
        {
        }
}

int main()
{
    char command[256];
    pid_t pid;

    printf(">> ");
    fgets(command, 256, stdin);
    char *commands[16];
    size_t number_of_commands;
    
    if(strchr(command, ';') != 0)
    {
        int count = 0;
        
        for(int i = 0; i < 256; i++)
        {
            if(command[i] == ';')
            {
                count++;
            }
        }

        char *tokens = strtok(command, ";");

        pid = fork();
        if(pid == 0)
        {
            number_of_commands = split(tokens, PIPE_DELIM, commands);
        }
        else
        {
            for(int i = 0; i < count; i++)
            {
                wait(NULL);
                tokens = strtok(NULL, ";");
                if(i < count - 1)
                {                
                    if(fork() == 0)
                    {
                        number_of_commands = split(tokens, PIPE_DELIM, commands);
                        break;
                    }
                }
                else
                {
                    number_of_commands = split(tokens, PIPE_DELIM, commands);
                }
            }
    
        }
    }
    else
    {
        number_of_commands = split(command, PIPE_DELIM, commands);
    }

    for(size_t i = 0; i < number_of_commands; ++i)
    {
        commands[i] = trim(commands[i]);

        if(strchr(commands[i], '>') != NULL)
        {
            commands[i] = removeRedirect(commands[i]);
        }
    }

    struct Pipe pipes[number_of_commands - 1];

    for(size_t i = 0; i < number_of_commands - 1; ++i)
    {
        if(pipe(pipes[i].fd) < 0)
        {
            perror("Pipe init failed");
            exit(EXIT_FAILURE);
        }
    }

    if(number_of_commands == 1)
    {
        char *singleCommands[16];
        number_of_commands = split(commands[0], SPACE_DELIM, singleCommands);
        for(int i = 0; i < number_of_commands; i++)
        {
            if(strchr(singleCommands[i], '>') != NULL)
            {
                singleCommands[i] = removeRedirect(singleCommands[i]);
            }

        }
        execvp(singleCommands[0], singleCommands);
        perror("Exec failed. Should never get here");
        exit(EXIT_FAILURE); 
    }
    else
    {
        for(size_t i = 0; i < number_of_commands; ++i)
        {
            if(i == 0) // first command
            {
                execute(i, commands[i], FALSE, TRUE, pipes);
            }
            else if(i == number_of_commands - 1) // last command
            {
                execute(i, commands[i], TRUE, FALSE, pipes);
                close(pipes[i].fd[READ]);
                close(pipes[i].fd[WRITE]);
            }
            else
            {
                execute(i, commands[i], TRUE, TRUE, pipes);
                close(pipes[i - 1].fd[READ]);
                close(pipes[i - 1].fd[WRITE]);
            }
        }
    }

    return 0;
}