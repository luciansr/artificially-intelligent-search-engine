while read title
do        
   curl -H "Content-Type: application/json" -XPOST "http://localhost:9200/search/offers" -d "{ \"title\" : \"${title}\"}"
done < /usr/share/elasticsearch/data/ecommerce_unique_names.txt