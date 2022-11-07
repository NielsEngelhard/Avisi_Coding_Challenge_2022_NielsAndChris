# Get all labels to print
import json

# return the wordnetsysnet
def get_all_labels_name():
    f = open(r"C:\Users\niels\Downloads\Reverse Captcha\labels.json")
  
    # returns JSON object as 
    # a dictionary
    data = json.load(f)

    labels_to_get = []
    for i in range(0, 1000): # because 1000 records
        #print(data[i]['label'])
        labels_to_get.append(data[i]['label'])
    f.close()
    #print(labels_to_get)
    return labels_to_get

# return the wordnetsysnet
def get_all_labels_wordnetsysnet():
    f = open(r"C:\Users\niels\Downloads\Reverse Captcha\labels.json")
  
    # returns JSON object as 
    # a dictionary
    data = json.load(f)

    labels_to_get = []
    for i in range(0, 1000): # because 1000 records
        #print(data[i]['label'])
        labels_to_get.append(data[i]['wordnetSynset'])
    f.close()
    #print(labels_to_get)
    return labels_to_get

import requests
import concurrent
from concurrent.futures import ThreadPoolExecutor

result_list = []

labels = get_all_labels_name()
loops = range(0, 1000)

test_url = 'http://localhost:3000/users'
real_url = 'https://europe-west1-coding-challenge-platform.cloudfunctions.net/ctf-reverse-captcha-5736741495373824'
threads = 24

failed_labels = []
failed_indexes = []
def get_label_info(index):
    # Prepare request to send for index
    label = labels[index]
    #image_path = "C:\ReversedCaptcha\Images\itemname\itemname1.jpg".replace("itemname", label) # non HD images
    image_path = "C:\ReversedCaptcha\HD-Images-label-name\itemname\itemname.jpg".replace("itemname", label) # non HD images
    
    # Open image to send along
    with open(image_path, "rb") as file:
        files = {'file': file}
        
        # Send the request
        response = requests.post(real_url, headers={"Authorization":"{5736741495373824}"}, files=files)
        json_response = response.json()
        #print(json_response)

        if response.status_code == 200:
            print("Successfully retrieved label " + label + " with score " + json_response["result"])
        else:
            failed_labels.append(label)
            failed_indexes.append(index)
            print("Something went wrong for label " + label)
        
        # Do something with the response
        json_response['label'] = label # add the label to the response data so we can look it up later
        
        
    return json_response

with ThreadPoolExecutor(max_workers=threads) as executor:
    future_to_url = {executor.submit(get_label_info, char) for char in loops}
    
    for future in concurrent.futures.as_completed(future_to_url):
        try:
            data = future.result()
            #print(data)
            result_list.append(data)
            #print(data['status'])
            #print(data['result'])
        except Exception as e:
            print("Something went wrong " + str(e))