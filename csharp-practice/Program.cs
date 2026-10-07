List<string> tasks = new List<string>();



while (true)
{
    Console.WriteLine("1. Add Task");
    Console.WriteLine("2. Show Tasks");
    Console.WriteLine("3. Delete Task");
    Console.WriteLine("4. Exit");

    string choice = Console.ReadLine() ?? "";


    switch (choice)
    {
        case "1":
            Console.WriteLine("Please write your task:");

            string task = Console.ReadLine() ?? "";

            if (task.Trim() == "")
            {
                Console.WriteLine("Task cannot be empty.");
                break;
            }

            tasks.Add(task);
            Console.WriteLine("Task added.");
            break;
    

         case "2":
            PrintTasks(tasks);
             break;

        case "3":
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks to delete.");
            break;

        }
           PrintTasks(tasks);
        

           Console.WriteLine("Enter the task number you want to delete:");
           string deleteInput = Console.ReadLine() ?? "";

    if (int.TryParse(deleteInput, out int taskNumber))
    {
        if (taskNumber >= 1 && taskNumber <= tasks.Count)
        {
            tasks.RemoveAt(taskNumber - 1);
            Console.WriteLine("Task " + taskNumber + " has been deleted.");
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid task number.");
    }

    break;

    case "4":
        Console.WriteLine("Goodbye!");
        return;

    default:
        Console.WriteLine("Invalid choice.");
        break;
} 
    } 
 /*Console.WriteLine("Please write your task: ");

 string task = Console.ReadLine() ?? "";

 if ( task == "exit" )
{
    break;
}


 Console.WriteLine("Your task is: " + task);
 
tasks.Add(task);


/*foreach (string task in tasks)
{
    Console.WriteLine("Task: " + task);
}


for (int i = 0; i < tasks.Count; i++)
{
    Console.WriteLine("Task " + (i + 1) + ": " + tasks[i]);
}

Console.WriteLine("Enter the task number you want to delete: ");
string deleteInput = Console.ReadLine() ?? "";
if (int.TryParse(deleteInput, out int taskNumber))
{
if (taskNumber >= 1 && taskNumber <= tasks.Count)
{
    tasks.RemoveAt(taskNumber - 1);
    Console.WriteLine("Task " + taskNumber + " has been deleted.");
}
else
{
    Console.WriteLine("Invalid task number.");
}
}
else
{
    Console.WriteLine("Invalid input. Please enter a valid task number.");
}

Console.WriteLine(" your tasks after deletion are: ");

PrintTasks(tasks);*/

/*Console.WriteLine("Show my tasks: ");*/

static void PrintTasks(List<string> tasks)
{
//print all Tasks here

if (tasks.Count == 0)
{
    Console.WriteLine("No tasks found.");
}
else
{
     // Your existing for loop goes here
     //printTasks(tasks);
     for (int i = 0; i < tasks.Count; i++)
{
    Console.WriteLine("Task " + (i + 1) + ": " + tasks[i]);
}
}  
}


