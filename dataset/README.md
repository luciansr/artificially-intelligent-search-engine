# How to run
```sh
docker build -t lucian/elasticsearch .
docker run --name elastic1 -d -p 9200:9200 -p 9300:9300 --restart unless-stopped lucian/elasticsearch
```

# Needed to run docker-compose
```sh
sudo sysctl -w vm.max_map_count=262144
```