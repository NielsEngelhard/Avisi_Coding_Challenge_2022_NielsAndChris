import json
import hashlib


def decrypt_dozen():
    with open('magazijn_decrypted.json') as magazijn:
        dozen = json.load(magazijn)

        for doos in dozen:
            content_hash = hashlib.sha256(doos["content"].encode()).hexdigest()
            for next_doos in dozen:
                if next_doos["next"] == content_hash:
                    next_doos["next"] = doos['id']
                    break

        return dozen


def order_dozen(dozen):
    ordered_dozen = []
    ordered_dozen.insert(0, "DPU")

    current = "DPU"
    while len(ordered_dozen) < len(dozen):
        for doos in dozen:
            if doos["next"] == current:
                ordered_dozen.insert(0, doos["id"])
                current = doos["id"]

    return ordered_dozen


def solve(dozen):
    flag = ""
    for doos in dozen:
        flag += doos
        if doos != "DPU":
            flag += "-"
    
    return flag


decrypted_dozen = decrypt_dozen()
ordered_dozen = order_dozen(decrypted_dozen)
flag = solve(ordered_dozen)
print(flag)