# Jump Mapper
Approximating PIN guess numbers using keypad patterns.

By training a model with an existing breached data set of 4-digit PINs and their frequencies, can we correctly predict the strength of previously unseen PINs using only keypad jumps? Can we extend this to longer PINs without adding any new training data?

## Usage
Briefly, use the program like this:

```
./JumpMapper.exe <training_file> <input_file> [format]
```

The options are quite straightforward:

| Option        | Values      | Required? | Description                                    |
|---------------|-------------|-----------|------------------------------------------------|
| training_file | Any         | Yes       | The file containing training data.             |
| input_file    | Any         | Yes       | The file containing unseen data.               |
| format        | `plain/coq` | No        | The format of output, defaults to `plain`.     |

Results are written to standard output. The `plain` format produced comma-delimited output with headings. The `coq` format option will produce a lookup-based (precomputed) valuation function and trie definition ready for use with [Skeptic](https://github.com/sr-lab/skeptic).

## Training
The program accepts semicolon/newline delimited files in the format:

```
pin1;frequency1
pin2;frequency2
pin3;frequency3
...
```

One such file can be found [here](http://jemore.free.fr/wordpress/?p=73&t=most-common-pin-numbers-complete-list). If only four-digit PINs are provided as training data, unseen data should be limited to 4-digit PINS only or there'll be errors. Here's a sample:

```
1234;255
1111;244
0000;221
1212;212
7777;203
...
```

## Unseen Data
Unseen data is provided as a text file with one PIN per line:

```
0000
0001
0002
0003
0004
0005
0006
0007
0008
0009
0010
...
```

## Output
Output is produced in the following `plain` format. Higher `vulnerability` means the PIN is a worse choice.

```
pin, vulnerability
0000, 118409
0001, 116563
0002, 116529
0003, 116521
0004, 116715
0005, 116817
...
```

## Credits
- PIN numbers sorted by frequency: http://jemore.free.fr/wordpress/?p=73&t=most-common-pin-numbers-complete-list
- Original PIN number frequency analysis: http://www.datagenetics.com/blog/september32012/
