$(document).ready(function () {
  const tbody = $("tbody");
  const addForm = $(".add-form");
  const inputTask = $(".input-task");
  const token = localStorage.getItem("token");
  const userId = localStorage.getItem("id");

  $('.input-task').hover(
    function () {
        $.notify('Para adicionar uma task apos digitar clique no icone "+" ou aperte enter para registrar a tarefa', "info")
    }, 
    function () {
        
    }
);

  var logoutButton = $(
    '<button id="logoutButton" class="btn-sair">Exit</button>'
  );

  logoutButton.on("click", function () {
    logout();
  });

  $("header").append(logoutButton);

  function logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("id");
    window.location.href = "/index.html";
  }

  const fetchTasks = async () => {
    try {
      const response = await fetch("http://localhost:5225/api/task", {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      });

      if (response.status !== 200) {
        $.notify("An error occurred while loading the data:", "error");
      } else {
        $.notify("Data loaded", "success");
      }

      return await response.json();
    } catch (error) {
      $.notify(`An error occurred while loading the data: ${error}`, "error");
    }
  };

  const addTask = async (event) => {
    event.preventDefault();

    const task = {
      name: inputTask.val(),
      status: 1,
      userId: userId,
    };

    await fetchTask("post", task);
    loadTasks();
    inputTask.val("");
  };

  const fetchTask = async (method, body) => {
    try {
      const response = await fetch("http://localhost:5225/api/task", {
        method: method,
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify(body),
      });

      if (response.status !== 200) {
        $.notify("An error occurred while created a new task", "error");
      } else {
        $.notify("Task created", "success");
      }

      return await response.json();
    } catch (error) {
      $.notify(`An error occurred while created a new task: ${error}`, "error");
    }
  };

  const deleteTask = async (id) => {
    try {
      const response = await fetch(`http://localhost:5225/api/task/${id}`, {
        method: "delete",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      });

      if (response.status !== 200) {
        $.notify("An error occurred while deleting", "error");
      } else {
        $.notify("Task deleted", "success");
      }

      loadTasks();
    } catch (error) {
      $.notify(`An error occurred while deleting: ${error}`, "error");
    }
  };

  const updateTask = async ({ id, name, status, data }) => {
    try {
      var body = { id, name, status: parseInt(status), userId, data };
      const response = await fetch(`http://localhost:5225/api/task/${id}`, {
        method: "put",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify(body),
      });

      if (response.status !== 200) {
        $.notify("An error occurred while updating the task", "error");
      } else {
        $.notify("Task updated", "success");
      }

      loadTasks();
    } catch (error) {
      $.notify(`An error occurred while updating the task: ${error}`, "error");
    }
  };

  const formatDate = (dateUTC) => {
    const options = { dateStyle: "long", timeStyle: "short" };
    return new Date(dateUTC).toLocaleString("en-US", options);
  };

  const createElement = (tag, innerText = "", innerHTML = "") => {
    const element = $(`<${tag}></${tag}>`);
    if (innerText) element.text(innerText);
    if (innerHTML) element.html(innerHTML);
    return element;
  };

  const createSelect = (value) => {
    const options = `
            <option value="1">Pending</option>
            <option value="2">In progress</option>
            <option value="3">Completed</option>
          `;

    const select = createElement("select", "", options);
    select.val(value);
    return select;
  };

  const createRow = (task) => {
    const { id, name, data, status } = task;

    const tr = createElement("tr");
    const tdName = createElement("td", name);
    const tdCreatedAt = createElement("td", formatDate(data));
    const tdStatus = createElement("td");
    const tdActions = createElement("td");

    const select = createSelect(status);

    select.on("change", function () {
      updateTask({ ...task, status: $(this).val() });
    });

    const editButton = createElement(
      "button",
      "",
      '<span class="material-symbols-outlined">edit</span>'
    );
    const deleteButton = createElement(
      "button",
      "",
      '<span class="material-symbols-outlined">delete</span>'
    );

    const editForm = createElement("form");
    const editInput = createElement("input");

    editInput.val(name);
    editForm.append(editInput);

    editForm.on("submit", function (event) {
      event.preventDefault();
      updateTask({ id, name: editInput.val(), status });
    });

    editButton.on("click", function () {
      tdName.empty();
      tdName.append(editForm);
    });

    editButton.addClass("btn-action");
    deleteButton.addClass("btn-action");

    deleteButton.on("click", function () {
      deleteTask(id);
    });

    tdStatus.append(select);

    tdActions.append(editButton);
    tdActions.append(deleteButton);

    tr.append(tdName);
    tr.append(tdCreatedAt);
    tr.append(tdStatus);
    tr.append(tdActions);

    return tr;
  };

  const loadTasks = async () => {
    const tasks = await fetchTasks();
    tasks.sort((a, b) => a.data.localeCompare(b.data));
    tbody.empty();

    tasks.forEach((task) => {
      const tr = createRow(task);
      tbody.append(tr);
    });
  };

  addForm.on("submit", addTask);
  loadTasks();
});
