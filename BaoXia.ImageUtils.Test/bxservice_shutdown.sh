
netstat -antlp|grep 8080
ps -ef |grep BaoXia.ImageUtils.Test|awk '{print $2}'|xargs kill -9
echo 服务已关机