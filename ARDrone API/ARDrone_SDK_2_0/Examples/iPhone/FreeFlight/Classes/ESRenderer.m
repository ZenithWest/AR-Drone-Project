//
//  ESRenderer.m
//  FreeFlight
//
//  Created by Frédéric D'HAEYER on 24/10/11.
//  Copyright Parrot SA 2009. All rights reserved.
//
#import "ESRenderer.h"

typedef struct _InterleavedVertex_
{
    GLfloat position[3]; // Vertex
    GLfloat texcoord[2];  // Texture Coordinates
} InterleavedVertex;

static InterleavedVertex const vertex[] =
{
    { {1.0f, -1.0f, -1.2f }, { 0.0f, 1.0f } },
    { {-1.0f, -1.0f, -1.2f }, { 0.0f, 0.0f } },
    { {1.0f,  1.0f, -1.2f }, { 1.0f, 1.0f } },
    { {-1.0f,  1.0f, -1.2f }, { 1.0f, 0.0f } }
};

static GLushort const indexes[] = { 0, 1, 2, 3 };
enum {
    UNIFORM_MODELVIEWMATRIX,
    NUM_UNIFORMS
};
GLint uniforms[NUM_UNIFORMS];

// attribute index
enum {
    ATTRIB_VERTEX,
    NUM_ATTRIBUTES
};

@interface ESRenderer (PrivateMethods)
- (BOOL) loadShaders;
- (void) createVBO;

@end

@implementation ESRenderer
// Create an ES 2.0 context
- (id) initWithFrame:(CGRect)frame andDrone:(ARDrone*)drone
{
    self = [super init];
	if (self)
	{
        defaultFramebuffer = 0;
        colorRenderbuffer = 0;

		context = [[EAGLContext alloc] initWithAPI:kEAGLRenderingAPIOpenGLES2];
        if (!context || ![EAGLContext setCurrentContext:context])
		{
            NSLog(@"Could not create context");
            [self release];
            return nil;
        }
		
        [EAGLContext setCurrentContext:context];
        [self loadShaders];
        
        glUseProgram(programId);
        video = [[OpenGLSprite alloc] initWithFrame:frame withScaling:FIT_X withProgram:programId withDrone:drone];
    }
	
	return self;
}

- (BOOL) resizeFromLayer:(CAEAGLLayer *)layer
{    
    GLsizei backingWidth, backingHeight;
    glGenFramebuffers(1, &defaultFramebuffer);
    glGenRenderbuffers(1, &colorRenderbuffer);
    
    // Create default framebuffer object. The backing will be allocated for the current layer in -resizeFromLayer
    [EAGLContext setCurrentContext:context];
    glBindFramebuffer(GL_FRAMEBUFFER, defaultFramebuffer);
    glBindRenderbuffer(GL_RENDERBUFFER, colorRenderbuffer);
    [context renderbufferStorage:GL_RENDERBUFFER fromDrawable:layer];
    glFramebufferRenderbuffer(GL_FRAMEBUFFER, GL_COLOR_ATTACHMENT0, GL_RENDERBUFFER, colorRenderbuffer);
    
    glGetRenderbufferParameteriv(GL_RENDERBUFFER, GL_RENDERBUFFER_WIDTH, &backingWidth);
    glGetRenderbufferParameteriv(GL_RENDERBUFFER, GL_RENDERBUFFER_HEIGHT, &backingHeight);
    
    if (glCheckFramebufferStatus(GL_FRAMEBUFFER) != GL_FRAMEBUFFER_COMPLETE)
	{
        NSLog(@"Failed to make complete framebuffer object %x", glCheckFramebufferStatus(GL_FRAMEBUFFER));
        return NO;
    }
    
    glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
    glViewport(0, 0, backingWidth, backingHeight);
    [self createVBO];
    
    return YES;
}

- (void) dealloc
{
	// tear down GL
    if(defaultFramebuffer)
    {
        glDeleteFramebuffers(1, &defaultFramebuffer);
        defaultFramebuffer = 0;
    }
    
    if(colorRenderbuffer)
    {
        glDeleteRenderbuffers(1, &colorRenderbuffer);
        colorRenderbuffer = 0;
    }
    
	// tear down context
	if ([EAGLContext currentContext] != nil)
        [EAGLContext setCurrentContext:nil];
	
	[context release];
	context = nil;
	
	[super dealloc];
}

- (BOOL) loadShaders
{
    GLuint vertShader, fragShader;
    NSString *vertShaderPathname, *fragShaderPathname;
    
    NSLog(@"%s:%d", __FUNCTION__, __LINE__);
    // Create shader program.
    programId = glCreateProgram();
    
    // Create and compile vertex shader.
    vertShaderPathname = [[NSBundle mainBundle] pathForResource:@"Shader" ofType:@"vsh"];
    if (!opengl_shader_compile(&vertShader, GL_VERTEX_SHADER, 1, [[NSString stringWithContentsOfFile:vertShaderPathname encoding:NSUTF8StringEncoding error:nil] UTF8String]))
    {
        NSLog(@"Failed to compile vertex shader 2");
        return FALSE;
    }
    
    // Create and compile fragment shader.
    fragShaderPathname = [[NSBundle mainBundle] pathForResource:@"Shader" ofType:@"fsh"];
    if (!opengl_shader_compile(&fragShader, GL_FRAGMENT_SHADER, 1, [[NSString stringWithContentsOfFile:fragShaderPathname encoding:NSUTF8StringEncoding error:nil] UTF8String]))
    {
        NSLog(@"Failed to compile fragment shader 2");
        return FALSE;
    }
    
    // Attach vertex shader to program.
    glAttachShader(programId, vertShader);
    
    // Attach fragment shader to program.
    glAttachShader(programId, fragShader);
    
    // Bind attribute locations.
    // This needs to be done prior to linking.
    glBindAttribLocation(programId, ATTRIB_VERTEX, "position");
    
    // Link program.
    if (!opengl_shader_link(programId))
    {
        NSLog(@"Failed to link program: %d", programId);
		opengl_shader_destroy(vertShader, fragShader, programId);
        return FALSE;
    }
    
    uniforms[UNIFORM_MODELVIEWMATRIX] = glGetUniformLocation(programId, "modelViewProjMatrix");

    // Release vertex and fragment shaders.
    opengl_shader_destroy(vertShader, fragShader, 0);
    
    return TRUE;
}

- (void)render:(ARDrone *)instance
{	
    [EAGLContext setCurrentContext:context];
    glClear(GL_COLOR_BUFFER_BIT);
    glFlush();
    if(instance != nil)
    {
        [video drawSelf];
    }
        
    [context presentRenderbuffer:GL_RENDERBUFFER];
}

- (void)setScreenOrientationRight:(BOOL)right
{
	[video setScreenOrientationRight:right];
}

- (void) createVBO
{
    GLuint textureUniform = glGetUniformLocation(programId, "texture");
    glUniform1i(textureUniform, 0);
    
    glGenTextures(2, textureId);
    printf("Video texture identifier : %d\n", textureId[0]);
    glActiveTexture(GL_TEXTURE0);
    glBindTexture(GL_TEXTURE_2D,textureId[0]);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
    
    printf("Video texture identifier : %d\n", textureId[1]);
    glActiveTexture(GL_TEXTURE1);
    glBindTexture(GL_TEXTURE_2D, textureId[1]);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
    // Sets up pointers and enables states needed for using vertex arrays and textures
    // Vertices
    glGenBuffers(1, &vertexBufferId);
    glBindBuffer(GL_ARRAY_BUFFER, vertexBufferId); 
    glBufferData(GL_ARRAY_BUFFER, sizeof(vertex), vertex, GL_STATIC_DRAW);
    glVertexAttribPointer(ARDRONE_ATTRIB_POSITION, 3, GL_FLOAT, GL_FALSE, sizeof(InterleavedVertex), (void*)offsetof(InterleavedVertex, position));
    glEnableVertexAttribArray(ARDRONE_ATTRIB_POSITION);
    glVertexAttribPointer(ARDRONE_ATTRIB_TEXCOORD, 2, GL_FLOAT, GL_FALSE, sizeof(InterleavedVertex), (void*)offsetof(InterleavedVertex, texcoord));
    glEnableVertexAttribArray(ARDRONE_ATTRIB_TEXCOORD);
    printf("Video vertex buffer identifier : %d\n", vertexBufferId);
    
    // Indexes
    glGenBuffers(1, &indexBufferId);
    glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, indexBufferId);
    glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indexes), indexes, GL_STATIC_DRAW);
    printf("Video index buffer identifier : %d\n", indexBufferId);
}

@end
