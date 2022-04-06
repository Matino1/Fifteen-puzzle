import pandas as pd
import matplotlib.pyplot as plt
import matplotlib.ticker as mticker

data = pd.read_csv("extractedData.txt", sep=' ')
depth1 = data[data["depth"] == 1]
depth2 = data[data["depth"] == 2]
depth3 = data[data["depth"] == 3]
depth4 = data[data["depth"] == 4]
depth5 = data[data["depth"] == 5]
depth6 = data[data["depth"] == 6]
depth7 = data[data["depth"] == 7]

plot_name = ['Ogółem', "A*", 'BFS', "DFS"]
plot_in_total = ["A*", 'BFS', "DFS"]
x_label = "Głębokość"

y_label = ["Maksymalna głębokość rekursji", "Stany odwiedzone", "Stany przetworzone", "Długość rozwiązania",   "Czas trwania [ms]"]

pd.set_option('display.max_columns', None)
pd.set_option('display.max_rows', None)

index = [1, 2, 3, 4, 5, 6, 7]
moves = ["RDUL","RDLU", "DRUL", "DRLU", "LUDR", "LURD", "ULDR", "ULRD"]
heuristics = ["manh", "hamm"]
scale = 2


bfsDf = pd.DataFrame(columns=moves, index=index)
dfsDf = pd.DataFrame(columns=moves, index=index)
astarDf = pd.DataFrame(columns=heuristics, index=index)

for k in range(5):
    fig, axs = plt.subplots(2, 2, figsize=(4 * scale, 4 * scale))
    bfsDf = pd.DataFrame(columns=moves, index=index)
    dfsDf = pd.DataFrame(columns=moves, index=index)
    astarDf = pd.DataFrame(columns=heuristics, index=index)
    df = pd.DataFrame(columns=plot_in_total, index=index)
    for i in range(7):
        depthData = pd.DataFrame(data[data["depth"] == i + 1])
        astr = depthData[depthData["strategy"] == "astr"]
        bfs = depthData[depthData["strategy"] == "bfs"]
        dfs = depthData[depthData["strategy"] == "dfs"]
        astr_avgs = astr.mean(axis=0)[k+2]
        bfs_avgs = bfs.mean(axis=0)[k+2]
        dfs_avgs = dfs.mean(axis=0)[k+2]

        in_total_avg = [astr_avgs, bfs_avgs, dfs_avgs]

        df.loc[i + 1] = (in_total_avg)

        moves_avg_tab_bfs = []
        moves_avg_tab_dfs = []
        for move in moves:
            move_value_bfs = bfs[bfs["heuristic/moves"] == move.lower()]
            move_value_dfs = dfs[dfs["heuristic/moves"] == move.lower()]

            move_avg_bfs = move_value_bfs.mean(axis=0)[k+2]
            move_avg_dfs = move_value_dfs.mean(axis=0)[k+2]

            moves_avg_tab_bfs.append(move_avg_bfs)
            moves_avg_tab_dfs.append(move_avg_dfs)

        bfsDf.loc[i + 1] = (moves_avg_tab_bfs)
        dfsDf.loc[i + 1] = (moves_avg_tab_dfs)

        moves_avg_tab_astar = []
        for element in heuristics:
            move_value_astar = astr[astr["heuristic/moves"] == element.lower()]

            heuristic_avg = move_value_astar.mean(axis=0)[k+2]

            moves_avg_tab_astar.append(heuristic_avg)

        astarDf.loc[i + 1] = (moves_avg_tab_astar)



        # print(f"ASTR avg values({i + 1}): \n")
        # print(astr_sLength_avg)
        # print("\n")
        # print(f"BFS avg values({i + 1}): \n")
        # print(bfs_sLength_avg)
        # print("\n")
        # print(f"DFS avg values({i + 1}): \n")
        # print(dfs_sLength_avg)
        # print("\n")
        # print("\n")



    ax = axs[0, 0]
    ax = df.plot.bar(rot=0, ax=ax)
    ax.set_title(plot_name[0], pad=10)
    ax.set_ylabel(y_label[k])
    ax.legend(fancybox=True, shadow=True, bbox_to_anchor=(1.01, 1.01))
    ax.set_yscale('log')
    ax.yaxis.set_major_formatter(mticker.ScalarFormatter())
    #ax.set_xticks([20, 200, 500])
    #ax.get_xaxis().set_major_formatter(mpl.ticker.ScalarFormatter())

    ax  = axs[0, 1]
    ax = astarDf.plot.bar(rot=0, ax=ax)
    ax.set_title(plot_name[1], pad=10)
    ax.legend(fancybox=True, shadow=True, bbox_to_anchor=(1.01, 1.01))

    ax = axs[1, 0]
    ax = bfsDf.plot.bar(rot=0, ax=ax)
    ax.set_title(plot_name[2], pad=10)
    ax.set_ylabel(y_label[k])
    ax.set_xlabel(x_label)
    ax.legend(fancybox=True, shadow=True, bbox_to_anchor=(1.01, 1.01))

    ax = axs[1, 1]
    ax = dfsDf.plot.bar(rot=0, ax=ax)
    ax.set_title(plot_name[3], pad=10)
    ax.set_xlabel(x_label)
    ax.legend(fancybox=True, shadow=True, bbox_to_anchor=(1.01, 1.01))



    fig.tight_layout()
    plt.show()
