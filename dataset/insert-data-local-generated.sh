ID=1

while IFS=$'\t' read -r title largura altura peso
do        
    clean_title=$(echo $title | sed 's/"/\\"/g')
    JSON="{ \"id\": $ID, \"title\" : \"$clean_title\", \"width\": $largura, \"height\": $altura, \"weight\": $peso }"
    #echo "$ID $JSON"
    curl -H "Content-Type: application/json" -XPUT "http://localhost:9200/index_offer/offer/$ID?pretty" -d "$JSON"
    ID=$(($ID+1))
done < generated_file.txt