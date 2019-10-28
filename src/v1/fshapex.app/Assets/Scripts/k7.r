# load dependences
library(readr)
library(cluster)
library(clValid)
library(mclust)
library(ggplot2)
library(FactoMineR)
library(factoextra)

# load data
points <- read_csv("C:/FShapeX/App/Dataset/points2.csv", col_types = cols(dataset = col_skip(), ethnicity = col_skip(), gender = col_skip()))

# variables
k <- 7

# prepare data
exprs <- scale(points[,-1])
rownames(exprs) <- points$file[1:547]

# check directory
mainDir <- paste("C:/FShapeX/App/Clusters/k", k, sep = "")
ifelse(!dir.exists(mainDir), dir.create(mainDir), FALSE)

set.seed(123)
res.hc <- eclust(exprs, "hclust", k = k, graph = FALSE)

res.pca <- PCA(exprs, ncp = k, graph = FALSE)
res.hcpc <- HCPC(res.pca, nb.clust = k, graph = FALSE)
fviz_cluster(res.hcpc, repel = TRUE, show.clust.cent = TRUE, palette = "jco", ggtheme = theme_minimal(), main = "Factor map", geom = "point")

points$cluster <- res.hc$cluster

png(paste(mainDir, "/", points$cluster, ".png", sep = ""))
plot(res.hcpc, choice = "3D.map")
dev.off()
