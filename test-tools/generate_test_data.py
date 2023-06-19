#!/usr/bin/env python3

import json
import tiktoken
import sys

# run the command
# ./generate_test_data.py input_text_only.json cl100k_base
# to get tokens for gpt4 model of the input_text_only.json file
# The output file will be of the following structure:
# [{ "text": "...", tokens: [ 1, ... ] }, ...]


if(len(sys.argv) < 3):
    print("Invalid number of arguments")
    print("Usage: ./generate_test_data.py <input_file> <model_name>")
    print("Example: ./generate_test_data.py input_text_only.json gpt2")
    exit(1)

file_name = sys.argv[1]
encoder_name = sys.argv[2]


with open(file_name) as f:
    data = json.load(f)

encoder = tiktoken.get_encoding(encoder_name)

results = []
for text in data:
    tokens = encoder.encode(text)
    result = {"text": text, "tokens": tokens}
    results.append(result)

with open('test_data_{}.json'.format(encoder_name), 'w') as output_file:
    json.dump(results, output_file)
