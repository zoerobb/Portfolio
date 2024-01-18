import argparse
from faker import Faker   # pip install Faker
from collections import OrderedDict
import signal
import time
import json
import requests           # pip install requests
from requests.packages.urllib3.exceptions import InsecureRequestWarning

requests.packages.urllib3.disable_warnings(InsecureRequestWarning)

# Set up a signal handler to catch CTRL-C.  When CTRL-C is pressed, the program will exit gracefully.
# Do this so we don't quit in the middle of sending a message
running = True
def signal_handler(signal, frame):
    print('Quitting...')
    global running
    running = False

signal.signal(signal.SIGINT, signal_handler)

import random

def main():
    parser = argparse.ArgumentParser(description='Order generator for HW 6: creates fictional orders and sends them to the indicated URL. Type CTRL-C to quit')
    parser.add_argument('url', type=str, help='URL of the endpoint to send orders to')
    parser.add_argument('rate', type=float, help='Rate of order generation in average number of orders per second')
    parser.add_argument('maxItemID', type=int, help='Maximum item ID to generate orders for')
    args = parser.parse_args()

    # Grab the arguments from the command line
    endpoint = args.url
    rate = args.rate
    maxItemID = args.maxItemID
    
    # Setup the ability to generate fake names for the orders
    locales = OrderedDict([
        ('en_US', 4),
        ('es_ES',  1),
        ('fr_FR',  1)
    ])
    fake = Faker(locales)
    
    while running:
        # Generate a random order
        order = generate_order(fake, maxItemID)
        order_json = json.dumps(order)
        print('POST {}'.format(order_json), end=' ')
        # Send the order
        headers = {'Content-Type': 'application/json'}
        r = requests.post(endpoint, data=order_json, headers=headers, verify=False)
        # Check response status code
        if r.status_code >= 400:
            print('Error: {} {}'.format(r.status_code, r.text))
        else:
            print('Success {}'.format(r.status_code))
        # Sleep for a random amount of time with an average of rate
        sleep_time = 1.0 / rate
        sleep_time += sleep_time * (2*random.random() - 1)
        time.sleep(sleep_time)
        

def generate_order(fake,maxid):
    name = fake.name().split(' ')[0]
    all_ids = [i for i in range(1,maxid)]
    random.shuffle(all_ids)
    items = [{'id': all_ids.pop(), 'qty': random.randint(1,3)} for i in range(random.randint(1,5))]
    delivery = random.randint(1,3)
    # there should not be any duplicate item ids (or they'd just have an increased qty)
    assert len(set([i['id'] for i in items])) == len(items)
    return {'store': 1, 'dlvy': delivery,'name': name, 'items': items}

if __name__ == '__main__':
    main()
