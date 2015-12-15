
INCLUDE_DIR = -I /usr/include/libxml2/
#LIB_DIR = -L <>/Build/bin/

FLAGS := -fPIC -fshort-wchar -std=c++0x


HEADERS_FILES = *.h
SOURCE_FILES  = *.cpp
OBJ_FILES     = *.o

OUTPUT_DIR := ../Build/bin
NAME := libQuickFix.a
TARGET := $(OUTPUT_DIR)/$(NAME)

$(TARGET): $(OBJ_FILES)
	mkdir -p $(OUTPUT_DIR)
	ar r $(TARGET) $(OBJ_FILES)

$(OBJ_FILES) : $(SOURCE_FILES)
	g++ $(INCLUDE_DIR) $(FLAGS) -c $(SOURCE_FILES)

$(SOURCE_FILES) : $(HEADER_FILES)

clean :
	rm -f *.o
	rm -f $(TARGET)
