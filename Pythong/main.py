import pandas as pd
import matplotlib.pyplot as plt

data = pd.read_csv("extractedData.txt", sep=' ')
depth1 = data[data["depth"] == 1]
depth2 = data[data["depth"] == 2]
depth3 = data[data["depth"] == 3]
depth4 = data[data["depth"] == 4]
depth5 = data[data["depth"] == 5]
depth6 = data[data["depth"] == 6]
depth7 = data[data["depth"] == 7]

pd.set_option('display.max_columns', None)
pd.set_option('display.max_rows', None)
astr_avgs = []
bfs_avgs = []
dfs_avgs = []
index = [1, 2, 3, 4, 5, 6, 7]
for i in range (7):
    depthData = pd.DataFrame(data[data["depth"] == i + 1])
    astr = depthData[depthData["strategy"] == "astr"]
    bfs = depthData[depthData["strategy"] == "bfs"]
    dfs = depthData[depthData["strategy"] == "dfs"]
    astr_avgs.append(astr.mean(axis=0)[2])
    bfs_avgs.append(bfs.mean(axis=0)[2])
    dfs_avgs.append(dfs.mean(axis=0)[2])
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

df = pd.DataFrame({'A*': astr_avgs,
                   'BFS': bfs_avgs,
                   'DFS': dfs_avgs}, index=index)
ax = df.plot.bar(rot=0)
plt.show()
