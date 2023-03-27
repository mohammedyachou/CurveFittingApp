# Curve Fitting App

This repository contains a WPF application that allows users to load data files containing pairs of x and y values, display them on a graph,
and fit the data using linear, exponential, or power function models.
The application utilizes the Math.NET Numerics library for curve fitting and OxyPlot for graph plotting.

## Features

- Load data files in CSV format with x and y value pairs
- Display data points on an interactive graph
- Choose from three curve fitting models: linear, exponential, or power function
- Perform curve fitting and display the fitted curve on the graph
- Show fitted coefficients for the selected model

## Getting Started

### Prerequisites

- .NET 6.0 SDK
- Visual Studio 2022 (or another compatible IDE)

### Installation

1. Clone the repository:

- git clone https://github.com/mohammedyachou/CurveFittingApp

2. Open the solution file `CurveFittingApp.sln` in Visual Studio.

3. Build the solution by right-clicking on the solution file in the Solution Explorer and selecting "Build Solution".

4. Set the `CurveFittingApp` project as the startup project by right-clicking on the project in the Solution Explorer and selecting "Set as StartUp Project".

5. Run the application in Visual Studio.

## Usage

1. Click the "Load Data" button to select a CSV file containing x and y value pairs.

2. The data points will be displayed on the graph.

3. Choose one of the three curve fitting models (linear, exponential, or power function) by selecting the corresponding radio button.

4. Click the "Fit" button to perform curve fitting using the selected model.

5. The fitted coefficients will be displayed below the "Fit" button, and the fitted curve will be plotted on the graph.

![1](https://user-images.githubusercontent.com/101590229/227933898-0e512bd3-510f-49df-9e82-0e8b7af2d144.PNG)
![2](https://user-images.githubusercontent.com/101590229/227933909-c3633495-221e-441a-bdba-324d891da1b7.PNG)
![3](https://user-images.githubusercontent.com/101590229/227933920-2cc78ac4-5831-44fa-9d4a-67b1a2bbcaaf.PNG)

