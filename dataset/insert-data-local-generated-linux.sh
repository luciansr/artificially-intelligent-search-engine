ID=1
SEPARATOR="\t"

while IFS='\t' read -r title largura altura peso;
do
    #echo $line | awk '{split($0,items,"\t"); title=$(items[0]);}'

    #title=$(items[0])
    #largura=$(items[1])
    #altura=$items[2]
    #peso=$items[3]
    
    clean_title=$(echo $title | sed 's/"/\\"/g')
    JSON="{ \"id\": $ID, \"title\" : \"$clean_title\", \"width\": $largura, \"height\": $altura, \"weight\": $peso }"
    echo "$ID $JSON"
    #curl -H "Content-Type: application/json" -XPUT "http://localhost:9200/index_offer/offer/$ID?pretty" -d "$JSON"
    ID=$(($ID+1))
done < generated_file.txt