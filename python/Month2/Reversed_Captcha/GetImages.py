#Imports Packages
from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By
import time
import base64
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.chrome.options import Options
import json

def open_browser_for_search():
    chrome_options = Options()
    chrome_options.add_experimental_option("detach", True)
    
    #Opens up web driver and goes to Google Images
    driver = webdriver.Chrome('C:/Driver/chromedriver.exe', options=chrome_options)
    driver.get('https://www.google.nl/')
    return driver

def close_cookie_popup(driver):
    cookie_popup = driver.find_element(By.XPATH, '//*[@id="L2AGLb"]/div')
    cookie_popup.click()  

def search_key_word(term_to_search, driver):
    box = driver.find_element(By.CLASS_NAME, "gLFyf")
    box.send_keys(term_to_search)
    box.send_keys(Keys.ENTER)

def click_on_first_image(driver):
    images_box = driver.find_element(By.XPATH, '//*[@id="hdtb-msb"]/div[1]/div/div[2]/a')
    images_box.click()

# function for saving a base64 image to the directory

def save_my_image(base64string, path):
    base64string = strip_base64_info_from_string(base64string)
    my_str_as_bytes = str.encode(base64string)
    with open(path, "wb") as fh:
        fh.write(base64.decodebytes(my_str_as_bytes))

def strip_base64_info_from_string(base64string):
    splitted_string = base64string.split(",")
    return splitted_string[1] # right side of the ,

def get_and_save_image(driver, path):
    try:
        image_base64 = driver.find_element(By.XPATH, '//*[@id="islrg"]/div[1]/div[1]/a[1]/div[1]/img').get_attribute("src")
        # print(image_base64)
        save_my_image(image_base64, path)
    except Exception as e:
        print("Error while scraping " + search_term)
        print("error message is: " + str(e))
        pass

def scrape_image(word):
    print("Scrapping " + word)
    save_images_path = 'C:\Driver\\' + word +  '.png'
    
    driver = open_browser_for_search()

    close_cookie_popup(driver)

    search_key_word(word, driver)

    click_on_first_image(driver)

    get_and_save_image(driver, save_images_path)
    time.sleep(2)
    driver.close()
    time.sleep(1)

# load json file as f
# Opening JSON file
f = open(r"C:\Users\niels\Downloads\Reverse Captcha\labels.json")
  
# returns JSON object as 
# a dictionary
data = json.load(f)

labels_to_get = []
for i in range(0, 1000): # because 1000 records
    #print(data[i]['label'])
    labels_to_get.append(data[i]['label'])
f.close()
print(labels_to_get)

for label in labels_to_get:
    print(label)
    scrape_image(label)