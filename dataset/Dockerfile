FROM docker.elastic.co/elasticsearch/elasticsearch:6.5.0

ENV discovery.type single-node
COPY ecommerce_unique_names.txt /usr/share/elasticsearch/data/

EXPOSE 9200
EXPOSE 9300

# RUN -- load data