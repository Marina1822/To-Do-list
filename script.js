//alert("this is my new to-do list");

//consttaskInput = document.querySelector("#taskInput");

 function addTask() {
  const taskText = taskInput.value.trim()};

//var taskList = document.querySelector("#taskList");



function addTask() { 
     const taskInput = document.querySelector("input");
     const taskText = taskInput.value;

     if (taskText.trim() === "") {
    return;
}

     const task = document.createElement("li");
     task.textContent = taskText;

     const deleteButton = document.createElement("button");
  deleteButton.textContent = "Delete";

   task.appendChild(deleteButton);

 deleteButton.addEventListener("click", function () {
    task.remove();
});

     const taskList = document.querySelector("ul");
     taskList.appendChild(task);
 
     taskInput.value = "";

     task.addEventListener("click", function() {
      task.classList.toggle("completed");
      

})}


function deleteTask() {
  const taskList = document.querySelector("ul");
  const lastTask = taskList.lastChild;


  

  if (lastTask) {
    taskList.removeChild(lastTask);
  }

  //const task = document.deleteTask("li");
  //taskList.appendChild(task);
}

deleteButton.addEventListener("click", function(event) {
         event.stopPropagation();
         task.remove();

         })