#Imports Packages
from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By
import time
import base64
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.common.keys import Keys
import json
import os
import requests
from urllib3.exceptions import InsecureRequestWarning
from urllib3 import disable_warnings

disable_warnings(InsecureRequestWarning)

# save image based on url with python
def save_image_based_on_url_and_label(image_url, label):
    create_directory_if_not_exists("C:\ReversedCaptcha\HD-Images-label-name\\" + label)
    
    save_path = 'C:\ReversedCaptcha\HD-Images-label-name\image_name\image_name.jpg'
    save_path = save_path.replace("image_name", label)
    
    img_data = requests.get(image_url, verify=False).content
    with open(save_path, 'wb') as handler:
        handler.write(img_data)
        
def create_directory_if_not_exists(path):
    if os.path.isdir(path): 
        return
    os.mkdir(path)
    
def open_browser_for_search():
    chrome_options = Options()
    chrome_options.add_experimental_option("detach", True)
    
    #Opens up web driver and goes to Google Images
    driver = webdriver.Chrome('C:/Driver/chromedriver.exe', options=chrome_options)
    driver.get('https://images.google.com/imghp?hl=nl&gl=nl')
    #print(driver)
    return driver

def close_cookie_popup(driver):
    cookie_popup = driver.find_element(By.XPATH, '//*[@id="L2AGLb"]/div')
    cookie_popup.click() 

def search_key_word(term_to_search, driver):
    box = driver.find_element(By.CLASS_NAME, "gLFyf")
    box.send_keys(term_to_search)
    box.send_keys(Keys.ENTER)
    
def open_browser_and_type_word(word):
    driver = open_browser_for_search()
    close_cookie_popup(driver)
    search_key_word(word, driver)
    #print(driver)
    return driver

def click_on_first_image(driver):
    images_box = driver.find_element(By.XPATH, '//*[@id="islrg"]/div[1]/div[1]/a[1]/div[1]/img')
    images_box.click()
    
def get_first_image_url(driver):
    time.sleep(2)
    url = WebDriverWait(driver, 20).until(EC.visibility_of_element_located((By.XPATH, '//*[@id="Sva75c"]/div/div/div[3]/div[2]/c-wiz/div/div[1]/div[1]/div[3]/div/a/img'))).get_attribute("src")
    return url

def type_new_word_in_search_bar(driver, word):
    images_box = driver.find_element(By.XPATH, '//*[@id="REsRA"]')
    images_box.click()
    
    images_box.send_keys(Keys.CONTROL + "a")
    images_box.send_keys(Keys.BACKSPACE)

    images_box.send_keys(word)
    images_box.send_keys(Keys.ENTER)
    
def click_and_save_first_image(driver, word):
    click_on_first_image(driver)
    url = get_first_image_url(driver)
    save_image_based_on_url_and_label(url, word)

def initialize_image_scraper(word):
    driver = open_browser_and_type_word(word)
    click_and_save_first_image(driver, word)
    #print(driver)
    return driver

def after_init_scrape(driver, word):
    type_new_word_in_search_bar(driver, word)
    click_and_save_first_image(driver, word)

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

labels = get_all_labels_name()
#print(len(labels))
start_index = 983

error_list = []
def scrape_images():
    d = initialize_image_scraper(labels[start_index])
    
    for x in range(start_index+1, 1000):
        try:
            time.sleep(1)
            after_init_scrape(d, labels[x])
        except Exception as e:
            try:
                d.quit()
                print(str(e) + " Failed for label " + labels[x])
                d = initialize_image_scraper(labels[x])
            except:
                error_list.append(x)
        print(error_list)
            
scrape_images()
