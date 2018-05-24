# install libs
install.packages("readr", dependencies = TRUE)
install.packages("clValid", dependencies = TRUE)
install.packages("mclust", dependencies = TRUE)
install.packages("FactoMineR", dependencies = TRUE)
install.packages("factoextra", dependencies = TRUE)

# load dependences
library(readr)
library(cluster)
library(clValid)
library(mclust)
library(ggplot2)
library(FactoMineR)
library(factoextra)

# load data
points <- read_csv("/home/ghost/MEGAsync/dataset front features/points.csv", col_types = cols(dataset = col_skip(), ethnicity = col_skip(), gender = col_skip()))

# variables
maxitems <- 300000
nclust <- 2:14
clmethods <- c("hierarchical","kmeans","pam")

# prepare data
exprs <- scale(points[,-1])
rownames(exprs) <- points$file[1:546]

# 
intern <- clValid(exprs, nClust = nclust, clMethods = clmethods, validation = "internal", maxitems = maxitems)
summary(intern)
plot(intern)
#      4        5        6       7
# 0.7882   0.7839   0.7824   0.7835

###

k <- 4

res.d <- dist(exprs, method="euclidean")
res.hc <- hclust(res.d, method="ward.D2")


# http://www.sthda.com/english/articles/25-cluster-analysis-in-r-practical-guide/111-types-of-clustering-methods-overview-and-quick-start-r-code/

# Hierarchical clustering
# fviz_dend(res.hc, k=k, cex=0.5, palette="jco", color_labels_by_k=TRUE, rect=TRUE)

# Clustering validation statistics
set.seed(123)
res.hc <- eclust(exprs, "hclust", k=k, graph=FALSE)
fviz_dend(res.hc, palette="jco", rect=TRUE, show_labels=FALSE)
fviz_silhouette(res.hc)

# http://www.sthda.com/english/articles/30-advanced-clustering/100-hierarchical-k-means-clustering-optimize-clusters/

#res.hk <-hkmeans(exprs, k)
#names(res.hk)
#fviz_dend(res.hk, cex=0.6, palette="jco", rect=TRUE, rect_border="jco", rect_fill=TRUE, show_labels=FALSE)
#fviz_cluster(res.hk, palette="jco", repel=TRUE, ggtheme=theme_classic(), geom="point")

# http://www.sthda.com/english/articles/22-principal-component-methods/74-hcpc-hierarchical-clustering-on-principal-components/

res.pca <- PCA(exprs, ncp=k, graph=FALSE)
res.hcpc <- HCPC(res.pca, nb.clust=k, graph=FALSE)
#fviz_dend(res.hcpc, show_labels=FALSE, palette="jco", rect=TRUE, rect_fill=TRUE, rect_border="jco", labels_track_height=0.8)
fviz_cluster(res.hcpc, repel=TRUE, show.clust.cent=TRUE, palette="jco", ggtheme=theme_minimal(), main="Factor map", geom="point")
plot(res.hcpc, choice="3D.map")




#groups <- cutree(res.hc, k=k)

points$cluster<-res.hc$cluster
rownames<-c("la","lb","lc","ld","le","lf","lg","lh","ra","rb","rc","rd","re","rf","rg","rh")
mainDir<-paste("/home/ghost/MEGAsync/dataset front features/k", k, sep="")
ifelse(!dir.exists(mainDir), dir.create(mainDir), FALSE)
#allPoints<-NULL
par(bg=NA)
for (i in 1:nrow(points)){
  file<-points[i,]$file
  
  clusterDir<-paste(mainDir, "/c", points[i,]$cluster, sep="")
  ifelse(!dir.exists(clusterDir), dir.create(clusterDir), FALSE)
  
  leftDir<-paste(clusterDir, "-l", sep="")
  ifelse(!dir.exists(leftDir), dir.create(leftDir), FALSE)
  
  rightDir<-paste(clusterDir, "-r", sep="")
  ifelse(!dir.exists(rightDir), dir.create(rightDir), FALSE)
  
  onlyPoints<-points[i, 2:33]

  odd<-onlyPoints[,seq(1,ncol(onlyPoints),2)]
  odd<-t(odd)
  colnames(odd)<-c("x")
  rownames(odd)<-rownames
  
  even<-onlyPoints[,seq(2,ncol(onlyPoints),2)]
  even<-t(even)
  colnames(even)<-c("y")
  rownames(even)<-rownames  

  newPoints=cbind(odd,even)
  write.csv(newPoints, file = paste(clusterDir, "/", file, ".csv", sep=""))
  
  write.csv(newPoints[1:8,], file = paste(leftDir, "/", file, ".csv", sep=""))
  left<-rbind(newPoints[1:8,],newPoints[1,])
  png(paste(leftDir, "/", file, ".png", sep=""))
  plot(left, type="o", lty="dashed",  axes=FALSE, xlab="", ylab="")
  dev.off()
  
  write.csv(newPoints[9:16,], file = paste(rightDir, "/", file, ".csv", sep=""))
  right<-rbind(newPoints[9:16,],newPoints[9,])
  png(paste(rightDir, "/", file, ".png", sep=""))
  plot(right, type="o", lty="dashed",  axes=FALSE, xlab="", ylab="")
  dev.off()
  
  #allPoints<-rbind(allPoints,newPoints)
}
#png(paste(clusterDir, "/cluster.png", sep=""))
#plot(allPoints, type="p", axes=FALSE, xlab="", ylab="")
#dev.off()





points <- read_csv("/home/vinicius/MEGAsync/dataset front features/k4")





only_points<-points[1, 2:33]

odd<-only_points[,seq(1,ncol(only_points),2)]
odd<-t(odd)
colnames(odd)<-c("x")
rownames(odd)<-rownames

even<-only_points[,seq(2,ncol(only_points),2)]
even<-t(even)
colnames(even)<-c("y")
rownames(even)<-rownames  

new_points=cbind(odd,even)

left=rbind(new_points[1:8,],new_points[1,])
right=rbind(new_points[9:16,],new_points[9,])
plot(right, type="o", lty="dashed",  axes=FALSE, xlab="", ylab="")



