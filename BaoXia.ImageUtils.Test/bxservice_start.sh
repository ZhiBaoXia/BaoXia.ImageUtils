netstat -antlp|grep 8080
ps -ef |grep BaoXia.ImageUtils.Test|awk '{print $2}'|xargs kill -9
export DOTNET_ROOT=/opt/soft/.dotnet
export PATH=$PATH:/opt/soft/.dotnet
nohup dotnet BaoXia.ImageUtils.Test.dll >/dev/null 2>&1 &
echo "服务已启动"