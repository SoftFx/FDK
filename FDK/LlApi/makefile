#INCLUDE_DIR = -I <>/include/
LIB_DIR = -L ../Build/bin/

FLAGS := -fPIC -fshort-wchar -std=c++0x
HEADERS_FILES = *.h
SOURCE_FILES  = *.cpp
OBJ_FILES     = *.o


OUTPUT_DIR := ../Build/bin
TARGET := $(OUTPUT_DIR)/libLlApi.so


$(TARGET) : $(OBJ_FILES)
	mkdir -p $(OUTPUT_DIR)
	g++ -shared -o $(TARGET) $(OBJ_FILES) $(LIB_DIR) -Wl,-rpath,. -lSal -lFixProvider -lCore

exe : $(OBJ_FILES)
	mkdir -p $(OUTPUT_DIR)
	g++ -o $(TARGET) $(OBJ_FILES) $(LIB_DIR) -Wl,-rpath,. -lSal -lFixProvider -lCore


$(OBJ_FILES) : $(SOURCE_FILES)
	g++ $(INCLUDE_DIR) $(FLAGS) -c $(SOURCE_FILES)

$(SOURCE_FILES) : $(HEADER_FILES)

clean :
	rm -f *.o
	rm -f $(TARGET)
