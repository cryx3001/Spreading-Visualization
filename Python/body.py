import math
import random


class Body:
	global all_instances
	all_instances = []

	def __init__(self, id, win, canv, inactive_rate):
		global couple_infected
		global detection_timer
		detection_timer = 0
		couple_infected = []
		self.id = id
		self.speed = [0, 0]
		self.center = [0, 0]
		self.size = 8
		self.win = win
		self.canv = canv
		self.ini_pos = [random.randrange(0, 800), random.randrange(0, 800)]
		self.draw = self.canv.create_oval(self.ini_pos[0], self.ini_pos[1], self.ini_pos[0] + self.size,
										  self.ini_pos[1] + self.size, width=1, outline="black",
										  fill="green")
		all_instances.append(self)
		if id == 0:
			self.type = 1  # 0:Healthy; 1:Sick;
			self.canv.itemconfig(self.draw, fill="red")
		else:
			self.type = 0

		n = random.randrange(0, 100)
		if n > inactive_rate:
			self.modify_speed()
			self.dep(True)
		else:
			self.dep(False)  # Even if it doesn't move, it "stimulates" the body so it can be detectable for collisions.

		self.detect_collision()

	def dep(self, active):
		self.canv.move(self.draw, self.speed[0], self.speed[1])
		self.check_position()
		if active:
			self.win.after(20, self.dep, True)
		else:
			self.win.after(10000, self.dep, False)

	def check_position(self):
		self.center = [self.canv.coords(self.draw)[0] + self.size / 2, self.canv.coords(self.draw)[1] + self.size / 2]
		win = [self.win.winfo_width(), self.win.winfo_height()]

		if self.center[0] <= 0:
			self.teleport(win[0], 0)
		elif self.center[0] >= self.win.winfo_width():
			self.teleport(-win[0], 0)
		elif self.center[1] <= 0:
			self.teleport(0, win[1])
		elif self.center[1] >= self.win.winfo_height():
			self.teleport(0, -win[1])

	def teleport(self, tp_x, tp_y):
		x, y = [self.canv.coords(self.draw)[0], self.canv.coords(self.draw)[2]], [self.canv.coords(self.draw)[1],
																				  self.canv.coords(self.draw)[3]]
		self.canv.coords(self.draw, x[0] + tp_x, y[0] + tp_y, x[1] + tp_x, y[1] + tp_y)

	def modify_speed(self):
		self.speed = [random.randrange(-2, 2), random.randrange(-2, 2)]
		self.win.after(random.randrange(100, 200), self.modify_speed)

	def get_distance(self, obj0, obj1):
		return math.sqrt(math.pow(obj1.center[0] - obj0.center[0], 2) + math.pow(obj1.center[1] - obj0.center[1], 2))

	def detect_collision(self):
		collision = self.canv.find_overlapping(self.canv.coords(self.draw)[0], self.canv.coords(self.draw)[1],
											   self.canv.coords(self.draw)[2], self.canv.coords(self.draw)[3])
		if len(collision) >= 2:
			for obj in all_instances:
				if obj != self and self.get_distance(self, obj) < self.size * 1.2 and (obj.type == 1 or self.type == 1):
					self.change_status(self)
					self.change_status(obj)

		self.win.after(200, self.detect_collision)

	def change_status(self, obj):
		obj.type = 1
		obj.canv.itemconfig(obj.draw, fill="red")
