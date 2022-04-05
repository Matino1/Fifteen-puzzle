// See https://aka.ms/new-console-template for more information
using pietnastka;

Node RootNode = new Node(0, args[2]);

switch (args[0].ToLower())
{
    case "bfs":
        RootNode.setAlgorithm(new BFS());
        break;

    case "dfs":
        RootNode.setAlgorithm(new DFS());
        break;

    case "astr":
        RootNode.setAlgorithm(new Astar());
        break;
}

if (args[0].ToLower() == "bfs" || args[0].ToLower() == "dfs")
{
    RootNode.setMovesOrder(args[1].ToCharArray());
    RootNode.getSolution();
}
else if (args[0].ToLower() == "astr")
{
    RootNode.getSolution(args[1].ToLower());
}

RootNode.saveSolutionToFile(args[3]);
RootNode.saveAdditionalInfoToFile(args[4]);


