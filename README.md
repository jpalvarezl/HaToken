## HaToken

![Hadouken!](https://media.giphy.com/media/px6X1e8dWxdsc/giphy.gif)

A C# implementation for OpenAI language models encoding

### Current state

- It seems that the BPE file contains a `Map` of `base64` strings to `integer` pairs.

- The regex doesn't seem to translate well. There is something that breaks, when trying to encode something directly in rust (see in the fork this specific [test](https://github.com/jpalvarezl/tiktoken/blob/190553a83e29ab327f9f7143b3692459081a5711/src/lib.rs#L650))

- Currently the regex is not separating the words nor is it encoding them base64 individually, and that seems to be the problem

### Way forward

- Focus on the encoders' regex for `tiktoken`.
- Write the regex correctly in a C# compliant way.
- Test with an external script. Meaning, table test different inputs and assert over the output of `tiktoken` vs `HaToken`

### Tips and tricks

- Python debugging is easy from VSCode. Just remember to edit the `launch.json` file so that `"justMyCode": false` then you will be able to dive into your local `pip` installation of `tiktoken`

- In order to do that either press F11 a lot, or open the files in VSCode for `tiktoken` and set your breakpoints there. Their locations should be something like `~/.local/lib/python3.8/site-packages/tiktoken/core.py`

- You will have to install `tiktoken` with `pip` like so:

```bash
pip3 install tiktoken
```

- Example `playground.py` file:

```python
import tiktoken
enc = tiktoken.get_encoding("cl100k_base")
result = enc.encode("Hello world")

print(result)
```
