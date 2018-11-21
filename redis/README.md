# How to run
```sh
docker build -t lucian/redis .
docker run --name redis1 -d -p 9500:6379 --restart unless-stopped lucian/redis
```