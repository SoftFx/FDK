
if(!require(roxygen2)){
	install.packages("roxygen2", repos="http://cran.us.r-project.org")
  require(roxygen2)
}

if(!require(devtools)){
  install.packages("devtools", repos="http://cran.us.r-project.org")
  require(devtools)
}

if(!require(data.table)){
  install.packages("data.table", repos="http://cran.us.r-project.org")
  require(data.table)
}

install.packages("dist/rClr_0.7-4.zip", repos = NULL, type = "win.binary")
require(rClr)

setwd("RPackage")
require(devtools)

devtools::document(roclets=c('rd', 'collate', 'namespace'))
packPath <- devtools::build(binary = TRUE, args = c('--preclean'))
