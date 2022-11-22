
## Using a virtual environment
<b>Step 1.</b> Create a new virtual environment 
<pre>
python -m venv rat_env
</pre> 
<br/>
<b>Step 2.</b> Activate your virtual environment
<pre>
source notebook-env/bin/activate # Linux
.\rat_env\Scripts\activate # Windows 
</pre>
<br/>
<b>Step 3.</b> Install dependencies and add virtual environment to the Python Kernel
<pre>
python -m pip install --upgrade pip
pip install ipykernel
python -m ipykernel install --user --name={{custom_name}}
</pre>
<br/>

## Using Jupyter Notebook
Install the following packages:
```
pip install jupyter

pip install notebook
```

After installing this you can the following command to launch jupyter notebook on your localhost:
```
jupyter notebook
```


<br/>