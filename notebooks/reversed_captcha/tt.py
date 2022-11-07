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
def get_all_labels_wordnetSynset():
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

wordnetSynset_labels = get_all_labels_wordnetSynset()

# create list of all image names per category (will be faster then calling os for each request)
import os

class ImageLocationItem:
    label: int
    location1: str
    location2: str
    location3: str
    location4: str
    location5: str
    def __init__(self):
        self.location1 = ""
        self.location2 = ""
        self.location4 = ""
        self.location3 = ""
        self.location5 = ""
    def PrintItem(self):
        print("for " + str(self.label))
        print("1: " + self.location1)
        print("2: " + self.location2)
        print("3: " + self.location3)
        print("4: " + self.location4)
        #print("5: " + self.location5) # most images only have max of 4 images available in directory
    def GetLocationList(self):
        locations = []
        if self.location1 != None:
            locations.append(self.location1)
        if self.location2 != None:
            locations.append(self.location2)
        if self.location3 != None:
            locations.append(self.location3)
        if self.location4 != None:
            locations.append(self.location4)
        return locations

item_labels = get_all_labels_name()
image_location_list = []
for x in range(0, 1000):
    item_name = item_labels[x]
    
    obj = ImageLocationItem()
    obj.label = item_name
    
    directory_in_str = "C:\ReversedCaptcha\hd-label-images\\" + item_name
    directory = os.fsencode(directory_in_str)
    
    currentIndex = 1
    for file in os.listdir(directory):
        filename = os.fsdecode(file)
         
        if currentIndex == 1:
            obj.location1 = directory_in_str + "\\" + filename # + directory? check when first time debugging
        if currentIndex == 2:
            obj.location2 = directory_in_str + "\\" + filename # + directory? check when first time debugging
        if currentIndex == 3:
            obj.location3 = directory_in_str + "\\" + filename # + directory? check when first time debugging
        if currentIndex == 4:
            obj.location4 = directory_in_str + "\\" + filename # + directory? check when first time debugging
        if currentIndex == 5:
            obj.location5 = directory_in_str + "\\" + filename # + directory? check when first time debugging
        
        if currentIndex == 6:
            break
        
        currentIndex += 1
    image_location_list.append(obj)

# Testing
#image_location_list[400].PrintItem()
#x = 1000
#for i in range(0, x): # if this does not fail, all images have at least 4 images 
    #image_location_list[i].PrintItem()

# image_location_list contains all image location
# item_labels contains all labels

import requests
import concurrent
from concurrent.futures import ThreadPoolExecutor

loops = range(0, 1000)

failed_labels = []
failed_indexes = []
dirs_to_remove = []

real_url = 'https://europe-west1-coding-challenge-platform.cloudfunctions.net/ctf-reverse-captcha-5736741495373824'

def get_label_info(index):
    # Prepare request to send for index
    label = item_labels[index]
    #image_path = "C:\ReversedCaptcha\Images\itemname\itemname1.jpg".replace("itemname", label) # non HD images
    image_path = image_location_list[index].location1 # HD images
    
    # Open image to send along
    with open(image_path, "rb") as file:
        files = {'file': file}
        
        # Send the request
        response = requests.post(real_url, headers={"Authorization":"{5736741495373824}"}, files=files)
        
        #print(json_response)

        json_response = None
        if response.status_code == 200:
            json_response = response.json()
            print(json_response["result"] + " " + label + " Successfully retrieved label " + label) #BIG_INFO
        elif response.status_code == 422:
            dirs_to_remove.append(image_path) # image is not sendable
        else:
            failed_labels.append(label)
            failed_indexes.append(index)
            #print("Something went wrong for label " + label + " with status code " + str(response.status_code)) BIG_INFO
        
        # Do something with the response
        json_response['label'] = label # add the label to the response data so we can look it up later
        json_response['index'] = index
        
    return json_response

def get_result_for_each_first_image():
    print("Getting results...")
    failed_labels = [] # global info lists to keep track off
    failed_indexes = []
    dirs_to_remove = []
    
    result_list = []
    test_url = 'http://localhost:3000/users'
    real_url = 'https://europe-west1-coding-challenge-platform.cloudfunctions.net/ctf-reverse-captcha-5736741495373824'
    threads = 23
    
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
                #print("Something went wrong " + str(e))
                pass
    print("Got all single results. Total of " + str(len(result_list)) + " records.")
    return result_list


            
def remove_image_from_dir(directory):
    #print("deleting " + directory)
    os.remove(directory)

def get_n_highest_of_list(result_list, n):
    print("sorting list of results with " +  str(len(result_list)) + " elements")

    result_list.sort(key=lambda x: x["result"])
    result_list.reverse()
    
    # print top x from the bottom (sort does sort )
    for i in range(0, 100):
        print(result_list[i])
        
    return result_list[0:n]

def sort_advanced_list(advanced_list):
    advanced_list.sort(key=lambda x: x.average_top_three)
    advanced_list.reverse()
    return advanced_list

# Remove false directories
def remove_false_dirs():
    print("n dirs to remove: " + str(len(dirs_to_remove)))
    for d in dirs_to_remove:
        remove_image_from_dir(d)

class AdvancedLabelData:
    label_index: int
    label_name: str
    max_value: float
    min_value: float
    average_total: float
    average_top_three: float
    total_dirs: int
    def LogThis(self):
        print("label_name: " + self.label_name)
        print("label_index: " + str(self.label_index))
        print("max_value: " + str(self.max_value))
        print("min_value: " + str(self.min_value))
        print("average_total: " + str(self.average_total))
        print("average_top_three: " + str(self.average_top_three))
        print("total_dirs: " + str(self.total_dirs))
        print("--------------------")

def get_advanced_label_data(label_name, index):
    all_results = get_all_label_results(label_name, index)
    #print(all_results)
    all_results_as_floats = str_list_to_float(all_results)
    all_results_as_floats.sort(reverse=True)
    
    advanced_data = AdvancedLabelData()
    
    advanced_data.label_index = index
    advanced_data.label_name = label_name
    advanced_data.total_dirs = len(all_results)
    
    advanced_data.max_value = all_results_as_floats[0]
    advanced_data.min_value = all_results_as_floats[len(all_results_as_floats)-1]
    
    # if 3+ records are in response set
    if len(all_results) >= 3:
        advanced_data.average_total = calc_average_total(all_results_as_floats)
        advanced_data.average_top_three = calc_average_total(all_results_as_floats[0:3])
    
    # if 2 records
    if len(all_results) == 2:
        advanced_data.average_total = calc_average_total(all_results_as_floats)
        advanced_data.average_top_three = calc_average_total(all_results_as_floats[0:2])
    
    # if only 1 is in response set
    if len(all_results) == 1:
        advanced_data.average_total = all_results_as_floats[0]
        advanced_data.average_top_three = None
    
    return advanced_data

def get_advanced_label_data_by_index(index):
    label_name = item_labels[index]
    
    all_results = get_all_label_results(label_name, index)
    
    all_results_as_floats = str_list_to_float(all_results)
    all_results_as_floats.sort(reverse=True)
    advanced_data = AdvancedLabelData()
    
    advanced_data.label_index = index
    advanced_data.label_name = label_name
    advanced_data.total_dirs = len(all_results)
    
    advanced_data.max_value = all_results_as_floats[0]
    advanced_data.min_value = all_results_as_floats[len(all_results_as_floats)-1]
    
    # if 3+ records are in response set
    if len(all_results) >= 3:
        advanced_data.average_total = calc_average_total(all_results_as_floats)
        advanced_data.average_top_three = calc_average_total(all_results_as_floats[0:3])
   
    # if 2 records
    if len(all_results) == 2:
        advanced_data.average_total = calc_average_total(all_results_as_floats)
        advanced_data.average_top_three = calc_average_total(all_results_as_floats[0:2])
    
    # if only 1 is in response set
    if len(all_results) == 1:
        advanced_data.average_total = all_results_as_floats[0]
        advanced_data.average_top_three = None
    
    return advanced_data

def get_all_label_results(label_name, index):
    label_image_dirs = image_location_list[index].GetLocationList()
    all_label_values = []
    for image_dir in label_image_dirs:
        try:
            resp = get_info_by_image_path(image_dir)
            all_label_values.append(resp["result"][0:4])
        except Exception as e:
            print(":-( " + str(e))
    return all_label_values
    
    
def get_info_by_image_path(image_path):
    # Open image to send along
    with open(image_path, "rb") as file:
        files = {'file': file}
        
        # Send the request
        response = requests.post(real_url, headers={"Authorization":"{5736741495373824}"}, files=files)
        json_response = response.json()
        #print(json_response)

        if response.status_code == 422:
            dirs_to_remove.append(image_path) # image is not sendable   
    return json_response

def str_list_to_float(str_list):
    n_list = []
    
    for s in str_list:
        try:
            n_list.append(float(s))
        except:
            #print("not a float (prob 'Could not parse... error because image cant send correctly'")
            pass
    return n_list

def calc_average_total(number_list):
    total = 0
    
    for number in number_list:
        total += number

    return total / len(number_list)

def get_indexes_from_list(the_list):
    index_list = []
    
    for rec in the_list:
        index_list.append(rec["index"])
    
    return index_list

def get_advanced_top_n_list(top_n, all_results):
    first_batch = get_result_for_each_first_image()
    list_top = get_n_highest_of_list(all_results, top_n)

    for record in list_top:
        label_name = record['label']
        index = item_labels.index(label_name)
        a = get_advanced_label_data(label_name, index)
        a.LogThis()
        
def get_adnvanced_data_by_index_list(index_list):
    advanced_result_list = []
    real_url = 'https://europe-west1-coding-challenge-platform.cloudfunctions.net/ctf-reverse-captcha-5736741495373824'
    threads = 23
    
    with ThreadPoolExecutor(max_workers=threads) as executor:
        future_to_url = {executor.submit(get_advanced_label_data_by_index, char) for char in index_list}

        for future in concurrent.futures.as_completed(future_to_url):
            try:
                data = future.result()
                #print(data)
                advanced_result_list.append(data)
                #print(data['status'])
                #print(data['result'])
            except Exception as e:
                print("Something went wrong " + str(e))    
    return advanced_result_list

def send_guess(index):
    label_wordnetsyns = wordnetSynset_labels[index]
    label_name = item_labels[index]
    print("sending guess for label " + label_name + " with synsnet " + label_wordnetsyns)
    return guess_answer(label_name, label_wordnetsyns)
                
def guess_answer(label, wordnet):
    url = "https://europe-west1-coding-challenge-platform.cloudfunctions.net/ctf-reverse-captcha-5736741495373824/answer"
    r = requests.post(url, headers={"Authorization":"{5736741495373824}"}, json={"label": label, "wordnetSynset": wordnet})

    if r.status_code == 500:
        print("Guess was wrong. Status code: " + str(r.status_code) + ". With message: " + str(r.content))
    else:
        print("YOOO, BIG ")
        print(r.status_code)
        print(r.content)
    return r.status_code != 500
    
def calc_labels_and_send_request(print_results):
    first_batch = get_result_for_each_first_image()
    highest_from_list = get_n_highest_of_list(first_batch, 50)
    index_list = get_indexes_from_list(highest_from_list)
    adv_results = get_adnvanced_data_by_index_list(index_list)
    sorted_adv_results = sort_advanced_list(adv_results)

    sorted_adv_results[0].LogThis()
    
    if print_results:
        for res in sorted_adv_results:
            res.LogThis()
            
    answer_correct = send_guess(sorted_adv_results[0].label_index)
    print("answer correct: " + str(answer_correct))
    return answer_correct

import requests
import time
from datetime import datetime

def send_static_request_with_same_image_always():
    # Prepare request to send for index
    label = "n09193705"
    image_path = "C:\ReversedCaptcha\hd-label-images\\accordion\\accordion1.jpg"
    real_url = 'https://europe-west1-coding-challenge-platform.cloudfunctions.net/ctf-reverse-captcha-5736741495373824'
    
    # Open image to send along
    with open(image_path, "rb") as file:
        files = {'file': file}
        
        # Send the request
        response = requests.post(real_url, headers={"Authorization":"{5736741495373824}"}, files=files)
        print("Static ping " + response.json()['result'])
        
        # Do something with the response
        json_response = response.json()
        json_response['label'] = label # add the label to the response data so we can look it up later
    return json_response

def send_request_and_get_response():
    r = send_static_request_with_same_image_always()
    return r['result']

current_result = ""
def ping_untill_other_response():
    result = send_request_and_get_response()
    current_result = result[0:6]
    
    for x in range(0, 60):
        result2 = send_request_and_get_response()
        compare_result = result2[0:6]
        
        if compare_result != current_result:
            return True
        time.sleep(20) # wait 20 seconds before trying again
        
    # If this returns false, something went completely wrong because in 30 minutes nothing changed?
    return False
    
def lets_do_this():
    stop_looping = False
    
    while stop_looping == False:
        ping_untill_other_response()
        correct = calc_labels_and_send_request(False)
        if correct:
            stop_looping = True

lets_do_this()



#y = [1]
#get_adnvanced_data_by_index_list(y)