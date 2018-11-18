awk -F "\"*,\"*" '{print $3}' kaggle_ecommerce.csv | while read unit_id relevance variance product_image product_link product_price product_title query rank source url product_description; do
  curl -d "cluster=${cluster}&username=${owner}&wallclock=${clock}" "$url" 
done



while read f1
do        
   curl -XPOST 'https://XXXXXXX.us-east-1.aws.found.io:9243/subway_info_v1/station' -H "Content-Type: application/json" -u elastic:XXXX -d "{ \"station\": \"$f1\" }"
done < kaggle_ecommerce.csv