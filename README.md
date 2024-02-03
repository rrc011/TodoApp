# Todo App - .NET 8 and Angular 15

Todo App is a task management application built with the latest technologies, utilizing .NET 8 on the server-side and Angular 15 on the client-side. It allows users to create lists of tasks, manage tasks within those lists, and perform operations like adding, editing, deleting, and completing tasks.

## Features

- **Task Lists:** Create and manage task lists for better organization.

- **Task Operations:** Perform various operations on tasks within lists, such as adding, editing, deleting, and marking as completed.

- **User-Friendly Interface:** A responsive and intuitive Angular 15 interface for seamless user interaction.

- **Real-time Updates:** Utilizes SignalR for real-time updates, ensuring users are instantly informed of changes.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) and [npm](https://www.npmjs.com/) for Angular development.

## Getting Started

1. Clone the repository:

    ```bash
    git clone https://github.com/rrc011/TodoApp.git
    cd TodoApp
    ```

2. Build and run the .NET backend:

    ```bash
    dotnet build
    dotnet run
    ```

3. Navigate to the `ClientApp` directory and install Angular dependencies:

    ```bash
    cd todo-app
    npm install
    ```

4. Run the Angular development server:

    ```bash
    ng serve
    ```

5. Open your browser and navigate to `http://localhost:4200/` to start using the Todo App.

## Usage

1. Create a Task List:
   - Click on "New List."
   - Enter a name for the list and click "Create."

2. Add Tasks to a List:
   - Select a list.
   - Click on "Add Task."
   - Enter task details and click "Save."

3. Edit and Delete Tasks:
   - Hover over a task and click "Edit" to modify details.
   - Click "Delete" to remove a task.

4. Complete Tasks:
   - Click the checkbox next to a task to mark it as completed.

## Configuration

Adjust the application settings in the `appsettings.json` file for the server-side and `environment.ts` for the client-side.

## Deployment

Follow the deployment guidelines for both .NET and Angular to deploy the Todo App in a production environment.

## Contributing

Feel free to contribute to the project by submitting issues or pull requests. Your feedback and contributions are highly appreciated.

## License

This project is licensed under the [MIT License](LICENSE).
