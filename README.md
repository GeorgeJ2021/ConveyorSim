# Conveyor Simulator
A modular conveyor belt simulation system built in Unity. The project allows users to dynamically place conveyor modules, connect them together, and simulate products moving through the conveyor network.

## Overview:
This project allows users to:

- Select and place different conveyor types into an environment.

- Automatically snap conveyors together to create a continuous conveyor line.

- Create custom conveyor layouts using straight, short, and inclined conveyor modules.

- Spawn different product types onto the conveyor system.

- Simulate products travelling across connected conveyors.

- Handle conveyor transitions, product movement, and physics-based interactions when products leave the conveyor line.

The goal of this project was to create a conveyor framework that can support dynamically created conveyor layouts rather than a fixed, pre-built scene.

## Features:
### Conveyor Placement:
- Runtime conveyor placement system.

- Free camera movement for navigating the environment.

- Conveyor snapping system using entry and exit connection points.

- Supports the following conveyor modules:

  - Standard/Generic Conveyor

  - Short Conveyor

  - Inclined Conveyor
### Conveyor Network: 
- Conveyors maintain connections with neighbouring conveyors.
- Products automatically transition between connected conveyor modules.

### Product Simulation

Currently supported products:

- Cardboard Box

- Metal Can

Products:

- Move smoothly along conveyor surfaces.

- Follow conveyor direction and inclination.

- Transition automatically between connected conveyors.

### Product Management

- Product spawning through UI controls.

- Maximum released product limit prevents unlimited object accumulation.

## Development Tools

### Engine

Unity 6

### Programming Language

C#

### IDE

Visual Studio Code

### Version Control

Git

GitHub

Git LFS (for large asset support)

### Unity Packages

Input System

TextMeshPro

## How to Run

### Requirements

Windows operating system

Unity 6 (for opening/editing the project)

### Running the Build

Extract the provided zip archive.

Open the Build folder.

Run the executable.

## Controls

### Camera:

- WASD - Move Camera
- Mouse + Right button (hold) - Look Around
   
### Conveyor Placement:
- Left Mouse Click - Place selected conveyor
- R - Rotate Conveyor

### UI:
- Spawn box button - spawns a cardboard box on the starting conveyor*
- Spawn Can button - spawns a metal can on the starting conveyor*
- Reset button - resets the entire scene, clears placed conveyors
- Quit button - quits the game.

*Spawn buttons have a 2-second cooldown to prevent excessive product spawning and maintain a stable simulation.



  
