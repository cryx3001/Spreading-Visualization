import tkinter
from body import Body

win = tkinter.Tk()
canv = tkinter.Canvas(win, width=600, height=600, bg="#c2c2c2")
list = []
i = 0


def generate_bodies(canv, rate):
	global i, list
	while i <= 200:
		list.append(Body(i, win, canv, rate))
		i = i + 1


def create_window():
	win.geometry("600x600")
	win.resizable(False, False)
	win.title("Spreading visualization")
	canv.pack()


if __name__ == "__main__":
	create_window()
	generate_bodies(canv, 0)
	win.mainloop()
