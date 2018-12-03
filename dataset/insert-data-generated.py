import requests 

def readFile():
    ID = 1
    with open('generated_file.txt') as f:
        for line in f:
            line = line.strip()
            values = line.split('\t')
            URL = 'http://localhost:9200/index_offer/offer/' + str(ID) + '?pretty'

            title = values[0]
            largura = values[1]
            altura = values[2]
            peso = values[3]

            title
            JSON = {
                'id': ID,
                'title': title,
                'width': int(largura),
                'height': int(altura),
                'weight': int(peso)
            }
            #print(JSON)
            
            r = requests.put(URL, json = JSON)
            print(r.text)
            ID = ID + 1
    
readFile()

