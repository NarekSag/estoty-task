# Space Invaders Modernization

## Technologies & Architecture
- **Dependency Injection**: VContainer for clean, modular dependency management
- **Async Programming**: UniTask for efficient, lightweight asynchronous operations
- **Design Pattern**: Composite Root architecture for centralized game setup

## Setup
1. Clone the repository
2. Open the project in Unity
3. Run the Bootstrap scene to initialize the game (Press [ALT + 1] on your keyboard to switch to Bootstrap scene)

## Configuration
Easily adjust game balance and parameters through `Config.json`, which is located in the `Assets/_Project/Resources` folder.

## Project Structure
The project is organized to enhance maintainability and facilitate future development. Below is an overview of the directory structure:

- **_Project**: Contains all project files, with third-party assets/libraries located outside this folder, which contributes to maintaining a clean and organized project structure.
  - **Scripts**: Divided into two main folders using assembly definitions:
    - **Editor**: Contains editor utilities for enhancing the development environment.
    - **Runtime**: Contains the core gameplay logic and utility classes.
      - **Utilities**: Various utility classes to assist with development.
  - **Bootstrap**: Responsible for bootstrapping the project and setting configurations. The process begins in the `BootstrapScene`, where the `BootstrapScope` registers services and configurations. The `BootstrapFlow` then loads these configurations and services before transitioning to the Core scene.
  - **Core**: Contains the main gameplay components:
    - In the `CoreScope`, gameplay-related components are registered. 
    - The `CoreFlow` handles asynchronous loading of these components, allowing for controlled order of initialization. This includes loading the `CoreController`, which initializes gameplay elements such as players, enemies, and views.
