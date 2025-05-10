const apiUrl = '/api/todos';

document.addEventListener('DOMContentLoaded', () => {
    loadTodos();

    document.getElementById('todoForm').addEventListener('submit', handleSubmit);
    document.getElementById('statusFilter').addEventListener('change', loadTodos);
    document.getElementById('cancelBtn').addEventListener('click', resetForm);
});

async function loadTodos() {
    const status = document.getElementById('statusFilter').value;
    const response = await fetch(`${apiUrl}?status=${status}`);
    const todos = await response.json();

    const container = document.getElementById('todoList');
    container.innerHTML = '';

    todos.forEach(todo => {
        const div = document.createElement('div');
        div.className = 'card mb-2';
        div.innerHTML = `
      <div class="card-body">
        <h5 class="card-title">${todo.title}</h5>
        <p class="card-text">${todo.description || ''}</p>
        <p>Status: ${['Pending', 'InProgress', 'Completed'][todo.status]} | 
           Priority: ${['Low', 'Medium', 'High'][todo.priority]}</p>
        <p>Due: ${todo.dueDate ? todo.dueDate.split('T')[0] : '-'}</p>
        <button class="btn btn-sm btn-success" onclick="completeTodo('${todo.id}')">Complete</button>
        <button class="btn btn-sm btn-warning" onclick="editTodo(${JSON.stringify(todo).replace(/"/g, '&quot;')})">Edit</button>
        <button class="btn btn-sm btn-danger" onclick="deleteTodo('${todo.id}')">Delete</button>
      </div>
    `;
        container.appendChild(div);
    });
}

async function handleSubmit(e) {
    e.preventDefault();

    const id = document.getElementById('todoId').value;
    const method = id ? 'PUT' : 'POST';
    const url = id ? `${apiUrl}/${id}` : apiUrl;

    const data = {
        title: document.getElementById('title').value,
        description: document.getElementById('description').value,
        status: parseInt(document.getElementById('status').value),
        priority: parseInt(document.getElementById('priority').value),
        dueDate: document.getElementById('dueDate').value || null
    };

    await fetch(url, {
        method,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });

    resetForm();
    loadTodos();
}

function editTodo(todo) {
    document.getElementById('formTitle').textContent = 'Edit Todo';
    document.getElementById('todoId').value = todo.id;
    document.getElementById('title').value = todo.title;
    document.getElementById('description').value = todo.description;
    document.getElementById('status').value = todo.status;
    document.getElementById('priority').value = todo.priority;
    document.getElementById('dueDate').value = todo.dueDate ? todo.dueDate.split('T')[0] : '';
}

function resetForm() {
    document.getElementById('formTitle').textContent = 'Add Todo';
    document.getElementById('todoForm').reset();
    document.getElementById('todoId').value = '';
}

async function deleteTodo(id) {
    if (confirm('Are you sure you want to delete this todo?')) {
        await fetch(`${apiUrl}/${id}`, { method: 'DELETE' });
        loadTodos();
    }
}

async function completeTodo(id) {
    await fetch(`${apiUrl}/${id}/complete`, { method: 'PATCH' });
    loadTodos();
}
