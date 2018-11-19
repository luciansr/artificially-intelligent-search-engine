FROM docker.elastic.co/elasticsearch/elasticsearch:6.5.0

ENV discovery.type single-node
COPY ecommerce_unique_names.txt /usr/share/elasticsearch/data/
COPY insert-data.sh /usr/share/elasticsearch/data/

EXPOSE 9200
EXPOSE 9300

RUN chmod +x /usr/share/elasticsearch/data/insert-data.sh

#CMD /usr/share/elasticsearch/data/insert-data.sh
