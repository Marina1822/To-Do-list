
async function addTask() {
    const taskInput = document.querySelector("input");
    const taskText = taskInput.value.trim();

    if (taskText === "") {
        return;
    }
    const response = await fetch("http://localhost:5256/todos", {
    method: "POST",
    headers: {
        "Content-Type": "application/json"
    },
    body: JSON.stringify({
        title: taskText,
        isCompleted: false
    })
});

 if (!response.ok) {
    console.error("Failed to create task");
    return;
}

    const newTask = await response.json();

    const task = document.createElement("li");
    task.textContent = newTask.title;

    const deleteButton = document.createElement("button");
    deleteButton.textContent = "Delete";

    task.appendChild(deleteButton);

    deleteButton.addEventListener("click", async function(event) {
    event.stopPropagation();

    const response = await fetch(
        `http://localhost:5256/todos/${newTask.id}`,
        {
            method: "DELETE"
        }
    );

    if (!response.ok) {
        console.error("Failed to delete task");
        return;
    }

    task.remove();
});

    task.addEventListener("click", async function() {

    const response = await fetch(
        `http://localhost:5256/todos/${newTask.id}`,
        {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                title: newTask.title,
                isCompleted: !newTask.isCompleted
            })
        }
    );

    if (!response.ok) {
        console.error("Failed to update task");
        return;
    }

    const updatedTask = await response.json();

    newTask.isCompleted = updatedTask.isCompleted;

    task.classList.toggle("completed", newTask.isCompleted);

});
         // 1. Send a PUT request

    // 2. Check response.ok

    // 3. Read the updated task from the response

    // 4. Update newTask.isCompleted

    // 5. Update the CSS class
       // task.classList.toggle("completed");
       // newTask.isCompleted = !newTask.isCompleted;
    

    const taskList = document.querySelector("ul");
    taskList.appendChild(task);

    taskInput.value = "";
}

function deleteTask() {
    const taskList = document.querySelector("ul");
    const lastTask = taskList.lastElementChild;

    if (lastTask) {
        taskList.removeChild(lastTask);
    }
}

async function loadTasks() {
    const response = await fetch("http://localhost:5256/todos");
    const tasks = await response.json();

    console.log(tasks);

    for (const task of tasks) {
        const taskElement = document.createElement("li");
        taskElement.textContent = task.title;

    if (task.isCompleted) {
        taskElement.classList.add("completed");
}

        const deleteButton = document.createElement("button");
        deleteButton.textContent = "Delete";

        taskElement.appendChild(deleteButton);

        deleteButton.addEventListener("click", async function(event) {
            event.stopPropagation();
            const response = await fetch(
                `http://localhost:5256/todos/${task.id}`,
                {
                    method: "DELETE"
                }
            );

            if (!response.ok) {
                const errorData = await response.json();
                console.error("Failed to delete task:", errorData);
                return;
            }

            taskElement.remove();
        });

        taskElement.addEventListener("click", async function() {
            const response = await fetch(
                `http://localhost:5256/todos/${task.id}`,
                {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        title: task.title,
                        isCompleted: !task.isCompleted
                    })
                }
            );

            if (!response.ok) {
                const errorData = await response.json();
                console.error("Failed to update task:", errorData);
                return;
            }

            const updatedTask = await response.json();
task.isCompleted = updatedTask.isCompleted;
taskElement.classList.toggle("completed", task.isCompleted);
        });

        const taskList = document.querySelector("ul");
        taskList.appendChild(taskElement);
    }
}

loadTasks();