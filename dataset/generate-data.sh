rm generated_file.txt

LINE=""
while read title
do        
    LARGURA=$((10+($RANDOM%20)*50))
    ALTURA=$((10+($RANDOM%20)*50))
    PESO=$((200+($RANDOM%15)*50))
    LINE="$title\t$LARGURA\t$ALTURA\t$PESO"
    echo "$LINE" >> generated_file.txt    
done < ecommerce_unique_names.txt
